using lego_indexor_api.API.Responses;
using lego_indexor_api.API.Services;
using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("api/v1/")]
public class PieceController : SecurityController
{
    public PieceController(IConnectionService connectionService) 
        : base(connectionService)
    { }
    
    [HttpPost("[controller]/add")]
    public ActionResult<AddPieceResponse> Index(AddPieceRequest request)
    {
        if (!Authenticate(request.Token))
            return Ok(new AddPieceResponse(false, false));

        var pieceService = new PieceService(UserId);
        
        return Ok(new AddPieceResponse(true, true));
    }
}