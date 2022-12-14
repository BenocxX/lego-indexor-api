using lego_indexor_api.Core.Models.DTOs;
using lego_indexor_api.Core.Models.DTOs.Pieces;
using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.Core.Models.Mappers;

public class AddPieceMapper : IMapper<Piece, AddPieceRequest>
{
    public AddPieceRequest ModelToRequest(Piece input)
    {
        return new AddPieceRequest
        {
            Type = input.Type,
            Name = input.Name,
            Count = input.Count,
            Description = input.Description
        };
    }

    public Piece RequestToModel(AddPieceRequest input)
    {
        return new Piece
        {
            Type = input.Type,
            Name = input.Name,
            Count = input.Count,
            Description = input.Description
        };
    }
}