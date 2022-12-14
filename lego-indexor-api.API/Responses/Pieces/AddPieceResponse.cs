namespace lego_indexor_api.API.Responses.Pieces;

public class AddPieceResponse : TokenResponse
{
    public bool IsSuccess { get; set; }
    
    public AddPieceResponse(bool isSuccess, bool isValidAuth) : base(isValidAuth)
    {
        IsSuccess = isSuccess;
    }
}