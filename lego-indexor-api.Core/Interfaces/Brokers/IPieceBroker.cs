using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Interfaces.Brokers;

public interface IPieceBroker
{
    IEnumerable<Piece> GetPieces();
    IEnumerable<Piece> GetPiecesByUserId(int userId);
    Piece? GetPieceById(int id);
    Piece? GetPieceByType(string type);
    Piece? UserHasPieceOfType(int userId, string type);
    Piece CreatePiece(Piece piece);
    Piece UpdateName(Piece piece, string? name);
    Piece UpdateCount(Piece piece, int? count);
    Piece UpdateDescription(Piece piece, string? description);
    Piece? DeletePieceById(int id);
}