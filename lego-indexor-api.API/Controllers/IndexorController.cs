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
    public IndexorController(IConnectionService connectionService) 
        : base(connectionService) { }
    
    [HttpPost("/scan")]
    public async Task<ActionResult<ScanResponse>> Scan(ScanRequest request)
    {
        if (!Authenticate(request.Token))
            return Ok(new ScanResponse(false, false));
        
        ClearImageDirectory();
        
        var indexorService = new IndexorService(UserId);
        
        if (!indexorService.AttachToServer())
            return Json(new { Status = 501, Message = "Web socket server not found." });

        await indexorService.TakePictures();
        var response = await indexorService.DownloadPictures();
        
        return Ok(new ScanResponse(true, true));
    }

    [HttpPost("/learn")]
    public async Task<ActionResult<ScanResponse>> TakePicture(LearnScanRequest request)
    {
        if (!Authenticate(request.Token))
            return Ok(new ScanResponse(false, false));
        
        ClearImageDirectory();

        var indexorService = new IndexorService(UserId);
        
        if (!indexorService.AttachToServer())
            return Json(new { Status = 501, Message = "Web socket server not found." });
        
        await indexorService.TakePictures();
        await indexorService.DownloadPictures($"learn/{request.FolderName}");
        
        return Ok(new ScanResponse(true, true));
    }
    
    private void ClearImageDirectory()
    {
        var directory = new DirectoryInfo("/Users/mathiscote/Ecole/Session 5/iot_III/lego/lego-indexor-api/lego-indexor-api.API/images");
        foreach (var file in directory.GetFiles())
            file.Delete(); 
    }
}