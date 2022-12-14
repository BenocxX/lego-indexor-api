using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Brokers;

public interface IPieceBroker
{
    IEnumerable<Piece> GetPieces();
    Piece? GetPieceById(int id);
    Piece CreatePiece(Piece piece);
}