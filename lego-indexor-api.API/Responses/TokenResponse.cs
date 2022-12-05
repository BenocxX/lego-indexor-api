namespace lego_indexor_api.API.Responses;

public class TokenResponse
{
    public bool IsValidAuth { get; set; }
    
    public TokenResponse(bool isValidAuth)
    {
        IsValidAuth = isValidAuth;
    }
}