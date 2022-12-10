using System.Text.Json;
using CliWrap.Buffered;
using lego_indexor_api.API.Websockets;
using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.Entities;
using lego_indexor_api.Core.Services;
using lego_indexor_api.Infrastructure.Brokers;

namespace lego_indexor_api.API.Services;

public class IndexorService
{
    private readonly int _userId;
    private readonly IRaspberryPiBroker _raspberryPiBroker;
    private WebSocketServer _server = null!;
    private RaspberryPi _raspberryPi = null!;

    public IndexorService(int userId)
    {
        _userId = userId;
        _raspberryPiBroker = new RaspberryPiBroker();
    }

    public bool AttachToServer()
    {
        var raspberryPi = _raspberryPiBroker.GetRaspberryPiByUserId(_userId);

        if (raspberryPi == null)
            return false;
        
        _raspberryPi = raspberryPi;
        
        var server = WebSocketManager.GetServer(new WebSocketConnection(_raspberryPi.MacAddress, true));
        if (server == null)
            return false;
        
        _server = server;
        return true;
    }

    public async Task TakePictures()
    {
        var message = new WebSocketResponse
        {
            messageType = MessageType.TakePicture,
            id = _raspberryPi.MacAddress
        };
        var jsonString = JsonSerializer.Serialize(message);
        await _server.Send(jsonString);
    }

    public async Task<WebSocketRequest> DownloadPictures(string path = "images")
    {
        _server.IsReading = false;
        
        var response = await GetServerResponse(_server);
        
        if (!Directory.Exists(path)) 
            Directory.CreateDirectory(path);
        
        await DownloadFileAsync($"http://{response.ip}/{response.fileCamTop}", $"{path}/{response.fileCamTop}");
        await DownloadFileAsync($"http://{response.ip}/{response.fileCamSide}", $"{path}/{response.fileCamSide}");
        
        var message = new WebSocketResponse
        {
            messageType = MessageType.PictureDownloaded,
            id = _raspberryPi.MacAddress,
            files = new []
            {
                response.fileCamTop,
                response.fileCamSide
            }
        };
        var jsonString = JsonSerializer.Serialize(message);
        
        await _server.Send(jsonString);
        
        _server.IsReading = true;
        
        return response;
    }

    public async Task<(string topPrediction, string sidePrediction)?> Predict(string topCamFile, string sideCamFile)
    {
        var commandLineService = new CommandLineService();
        var result = await commandLineService.RunPython($"../lego-indexor-api.Core/machine-learning/predict.py {topCamFile} {sideCamFile}");
        if (result.ExitCode != 0)
        {
            Console.WriteLine(result.StandardError);
            return null;
        }
        
        var output = result.StandardOutput;
        
        const string startIndicator = "PREDICTION: [";
        const string endIndicator = "]";
        var predictionIndex = output.LastIndexOf(startIndicator, StringComparison.Ordinal) + startIndicator.Length;
        var endPredictionIndex = output.LastIndexOf(endIndicator, StringComparison.Ordinal);
        var prediction = output[predictionIndex..endPredictionIndex];

        var predictions = prediction.Split(" | ");
        var topPrediction = predictions[0];
        var sidePrediction = predictions[1];
        
        return (topPrediction, sidePrediction);
    }
    
    private async Task<WebSocketRequest> GetServerResponse(WebSocketServer server)
    {
        do
        {
            var response = await server.ReadJson();
            if (response?.messageType == MessageType.PictureTaken)
                return response;
        } while (true);
    }
    
    private async Task DownloadFileAsync(string uri, string outputPath)
    {
        var client = new HttpClient();

        if (!Uri.TryCreate(uri, UriKind.Absolute, out _))
            throw new InvalidOperationException("URI is invalid.");
        
        var fileBytes = await client.GetByteArrayAsync(uri);
        await File.WriteAllBytesAsync(outputPath, fileBytes);
    }
}