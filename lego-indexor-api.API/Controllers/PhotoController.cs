using System.Text.Json;
using lego_indexor_api.API.Responses;
using lego_indexor_api.API.Websockets;
using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PhotoController : SecurityController
{
    private readonly IRaspberryPiBroker _raspberryPiBroker;
    
    public PhotoController(IConnectionService connectionService,
        IRaspberryPiBroker raspberryPiBroker) 
        : base(connectionService)
    {
        _raspberryPiBroker = raspberryPiBroker;
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(ScanRequest request)
    {
        if (!Authenticate(request.Token))
            return Ok(new ScanResponse(false, false));
        
        ClearImageDirectory();

        var raspberrypi = _raspberryPiBroker.GetRaspberryPiByUserId(UserId);
        
        if (raspberrypi == null)
            return Ok(new ScanResponse(false, true));

        var server = WebSocketManager.GetServer(new WebSocketConnection(raspberrypi.MacAddress, true));
        if (server == null)
            return Json(new { Status = 501, Message = "Web socket server not found." });

        var message = new WebSocketResponse
        {
            messageType = MessageType.TakePicture,
            id = raspberrypi.MacAddress
        };
        var jsonString = JsonSerializer.Serialize(message);
        await server.Send(jsonString);
        
        server.IsReading = false;

        var response = await GetServerResponse(server);

        await DownloadFileAsync(response.url!, $"images/{response.fileName}");
        
        server.IsReading = true;
        return Json(new
        {
            Status = 200, 
            FileName = response.fileName,
            Ip = response.ip,
            Url = response.url
        });
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
        await System.IO.File.WriteAllBytesAsync(outputPath, fileBytes);
    }

    private void ClearImageDirectory()
    {
        var directory = new DirectoryInfo("/Users/mathiscote/Ecole/Session 5/iot_III/lego/lego-indexor-api/lego-indexor-api.API/images");
        foreach (var file in directory.GetFiles())
            file.Delete(); 
    }
}
