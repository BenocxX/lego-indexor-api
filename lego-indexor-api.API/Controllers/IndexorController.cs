using lego_indexor_api.API.Responses;
using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class IndexorController : SecurityController
{
    private readonly IIndexorBroker _indexorBroker;
    
    public IndexorController(IIndexorBroker indexorBroker, 
        IConnectionService connectionService) 
        : base(connectionService)
    {
        _indexorBroker = indexorBroker;
    }
    
    [HttpPost("/scan")]
    public ActionResult<ScanResponse> Scan(ScanRequest request)
    {
        if (!Authenticate(request.Token))
            return Ok(new ScanResponse(false));

        // var indexor = _indexorBroker.GetIndexorByUserId(UserId);
        
        return Ok(new ScanResponse(true));
    }
}