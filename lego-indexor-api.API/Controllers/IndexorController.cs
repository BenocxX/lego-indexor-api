using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Interfaces.Services;
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
    public ActionResult<string> Scan(string token)
    {
        if (!Authenticate(token))
            return Ok("Invalid token");

        var indexor = _indexorBroker.GetIndexorByUserId(UserId);
        
        return Ok("Yes");
    }
}