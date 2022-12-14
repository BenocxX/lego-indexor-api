using lego_indexor_api.API.Responses;
using lego_indexor_api.API.Responses.Pieces;
using lego_indexor_api.API.Services;
using lego_indexor_api.Core.Interfaces.Services;
using lego_indexor_api.Core.Models.DTOs;
using lego_indexor_api.Core.Models.DTOs.Pieces;
using lego_indexor_api.Core.Models.Entities;
using lego_indexor_api.Core.Models.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace lego_indexor_api.API.Controllers;

[ApiController]
[Route("api/v1/")]
public class PieceController : SecurityController
{
    public PieceController(IConnectionService connectionService)
        : base(connectionService)
    { }

    [HttpPost("[controller]")]
    public ActionResult<GetPiecesResponse> Get(GetPiecesRequest request)
    {
        if (!Authenticate(request.Token))
            return Ok(new AddPieceResponse(false, false));
        
        var pieceService = new PieceService(UserId);
        var pieces = pieceService.GetAll();

        return Ok(new GetPiecesResponse(true, pieces));
    }
    
    [HttpPost("[controller]/add")]
    public ActionResult<AddPieceResponse> Index(AddPieceRequest request)
    {
        if (!Authenticate(request.Token))
            return Ok(new AddPieceResponse(false, false));

        var pieceService = new PieceService(UserId);
        pieceService.Add(request);
        
        return Ok(new AddPieceResponse(true, true));
    }
    
    [HttpDelete("[controller]/{pieceId}")]
    public ActionResult<DeletePieceResponse> Delete(DeletePieceRequest request, int pieceId)
    {
        if (!Authenticate(request.Token))
            return Ok(new DeletePieceResponse(false));
        
        var pieceService = new PieceService(UserId);
        pieceService.Delete(pieceId);

        return Ok(new DeletePieceResponse(true));
    }
}