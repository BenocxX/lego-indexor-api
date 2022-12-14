using lego_indexor_api.Core.Models.Entities;

namespace lego_indexor_api.API.Responses.Pieces;

public class GetPiecesResponse : TokenResponse
{
    public IEnumerable<Piece> Pieces { get; set; }

    public GetPiecesResponse(bool isValidAuth, IEnumerable<Piece> pieces) : base(isValidAuth)
    {
        Pieces = pieces;
    }
}