using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.DTOs;
using lego_indexor_api.Core.Models.DTOs.Pieces;
using lego_indexor_api.Core.Models.Entities;
using lego_indexor_api.Core.Models.Mappers;
using lego_indexor_api.Infrastructure.Brokers;

namespace lego_indexor_api.API.Services;

public class PieceService
{
    private readonly int _userId;
    private readonly IPieceBroker _pieceBroker;
    private readonly IMapper<Piece, AddPieceRequest> _mapper;

    public PieceService(int userId)
    {
        _userId = userId;
        _pieceBroker = new PieceBroker();
        _mapper = new AddPieceMapper();
    }

    public IEnumerable<Piece> GetAll()
    {
        return _pieceBroker.GetPiecesByUserId(_userId);
    }

    public void Add(AddPieceRequest request)
    {
        var piece = _mapper.RequestToModel(request);
        piece.UserId = _userId;

        var currentPiece = _pieceBroker.UserHasPieceOfType(_userId, piece.Type);
        if (currentPiece == null)
        {
            _pieceBroker.CreatePiece(piece);
        }
        else
        {
            _pieceBroker.UpdateName(currentPiece, piece.Name);
            _pieceBroker.UpdateCount(currentPiece, piece.Count);
            _pieceBroker.UpdateDescription(currentPiece, piece.Description);
        }
    }
}