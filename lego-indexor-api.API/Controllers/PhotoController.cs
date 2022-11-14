using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PhotoController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string id)
    {
        var server = WebSocketManager.GetServer(new WebSocketConnection(id, true));
        if (server == null)
            return Json(new { Status = 501, Message = "Web socket server not found." });

        var message = new WebSocketResponse
        {
            messageType = MessageType.TakePicture,
            id = id
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

    [HttpGet("Download")]
    public async Task<IActionResult> DownloadImage()
    {
        await DownloadFileAsync("http://10.3.3.66/img.png", "images/img.png");
        return Ok();
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
}
