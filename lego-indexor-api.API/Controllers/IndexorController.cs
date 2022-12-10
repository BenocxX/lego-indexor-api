using lego_indexor_api.API.Responses;
using lego_indexor_api.API.Services;
using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class IndexorController : SecurityController
{
    private ICommandLineService _commandLineService;
    
    public IndexorController(IConnectionService connectionService,
        ICommandLineService commandLineService)
        : base(connectionService)
    {
        _commandLineService = commandLineService;
    }

    [HttpPost("/scan")]
    public async Task<ActionResult<ScanResponse>> Scan(ScanRequest request)
    {
        if (!Authenticate(request.Token))
            return Ok(new ScanResponse(false, false));
        
        ClearImageDirectory();
        
        var indexorService = new IndexorService(UserId);
        
        if (!indexorService.AttachToServer())
            return Json(new { Status = 501, Message = "Web socket server not found." });

        Console.WriteLine("Taking pictures");
        await indexorService.TakePictures();

        Console.WriteLine("Downloading");
        var response = await indexorService.DownloadPictures();

        Console.WriteLine("Predicting");
        var prediction = await indexorService.Predict($"./images/{response.fileCamTop}");
        
        if (string.IsNullOrEmpty(prediction)) 
            return Ok(new ScanResponse(false, true));
        
        Console.WriteLine($"Prediction: {prediction}");
        
        return Ok(new ScanResponse(true, true));
    }

    [HttpPost("/learn")]
    public async Task<ActionResult<ScanResponse>> Learn(LearnScanRequest request)
    {
        if (!Authenticate(request.Token))
            return Ok(new ScanResponse(false, false));
        
        var indexorService = new IndexorService(UserId);
        
        if (!indexorService.AttachToServer())
            return Json(new { Status = 501, Message = "Web socket server not found." });
        
        for (var i = 0; i < 5; i++)
        {
            var index = i + 1;
            await indexorService.TakePictures();
            await indexorService.DownloadPictures($"learn/{request.FolderName}");
            Console.WriteLine($"Picture #{index} has been saved!");
        }
        
        Console.WriteLine("Done! You can now move the LEGO piece a little bit.");
        
        return Ok(new ScanResponse(true, true));
    }
    
    private void ClearImageDirectory()
    {
        var directory = new DirectoryInfo("/Users/mathiscote/Ecole/Session 5/iot_III/lego/lego-indexor-api/lego-indexor-api.API/images");
        foreach (var file in directory.GetFiles())
            file.Delete(); 
    }
}