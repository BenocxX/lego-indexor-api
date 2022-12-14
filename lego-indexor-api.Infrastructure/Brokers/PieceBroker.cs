using lego_indexor_api.Core.Interfaces.Brokers;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Infrastructure.Brokers;

public class PieceBroker : Broker, IPieceBroker
{
    public IEnumerable<Piece> GetPieces()
    {
        return Database.Pieces.ToList();
    }

    public IEnumerable<Piece> GetPiecesByUserId(int userId)
    {
        return Database.Pieces.ToList().Where(p => p.UserId == userId);
    }

    public Piece? GetPieceById(int id)
    {
        return Database.Pieces.Find(id);
    }

    public Piece? GetPieceByType(string type)
    {
        return Database.Pieces.FirstOrDefault(p => p.Type == type);
    }

    public Piece? UserHasPieceOfType(int userId, string type)
    {
        return Database.Pieces.FirstOrDefault(p => p.Type == type && p.UserId == userId);
    }

    public Piece CreatePiece(Piece piece)
    {
        var newPiece = Database.Pieces.Add(piece).Entity;
        Database.SaveChanges();
        return newPiece;
    }

    public Piece UpdateName(Piece piece, string? name)
    {
        if (name != "")
        {
            piece.Name = name;
            Database.SaveChanges();
        }
        return piece;
    }

    public Piece UpdateCount(Piece piece, int? count)
    {
        if (count > 1 && count != piece.Count)
            piece.Count = count;
        else
            piece.Count++;
        Database.SaveChanges();
        return piece;
    }

    public Piece UpdateDescription(Piece piece, string? description)
    {
        if (description != "")
        {
            piece.Description = description;
            Database.SaveChanges();
        }
        return piece;
    }
}