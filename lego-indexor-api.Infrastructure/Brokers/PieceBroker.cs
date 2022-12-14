using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Infrastructure.Brokers;

public class PieceBroker : Broker, IPieceBroker
{
    public IEnumerable<Piece> GetPieces()
    {
        return Database.Pieces.ToList();
    }

    public Piece? GetPieceById(int id)
    {
        return Database.Pieces.Find(id);
    }

    public Piece CreatePiece(Piece piece)
    {
        var newPiece = Database.Pieces.Add(piece).Entity;
        Database.SaveChanges();
        return newPiece;
    }
}