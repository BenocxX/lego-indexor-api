namespace lego_indexor_api.API.Responses;

public class AuthenticationResponse
{
    public bool IsSuccess { get; set; }
    public string? Details { get; set; }
    public int? UserId { get; set; }
    public string? Token { get; set; }
    
    public AuthenticationResponse(bool isSuccess, string? details = null, int? userId = null, string? token = null)
    {
        IsSuccess = isSuccess;
        Details = details;
        UserId = userId;
        Token = token;
    }
}