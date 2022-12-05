using System.Text.Json;
using lego_indexor_api.API.Websockets;
using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.Entities;
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
        
        _server.IsReading = true;
        return response;
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