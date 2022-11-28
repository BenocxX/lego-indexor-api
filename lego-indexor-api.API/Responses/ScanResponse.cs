namespace lego_indexor_api.API.Responses;

public class ScanResponse
{
    public bool IsSuccess { get; set; }
    
    public ScanResponse(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
}