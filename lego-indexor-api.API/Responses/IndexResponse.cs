namespace lego_indexor_api.API.Responses;

public class IndexResponse : TokenResponse
{
    public bool IsSuccess { get; set; }
    
    public IndexResponse(bool isSuccess, bool isValidAuth) : base(isValidAuth)
    {
        IsSuccess = isSuccess;
    }
}