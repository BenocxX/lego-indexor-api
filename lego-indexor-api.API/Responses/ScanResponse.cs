namespace lego_indexor_api.API.Responses;

public class ScanResponse : TokenResponse
{
    public bool IsSuccess { get; set; }
    
    public ScanResponse(bool isSuccess, bool isValidAuth)
        : base(isValidAuth)
    {
        IsSuccess = isSuccess;
    }
}