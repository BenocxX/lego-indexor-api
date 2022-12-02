using System.Text;
using lego_indexor_api.Core.Interfaces.Services;

namespace lego_indexor_api.Core.Services;

public class SecurityService : ISecurityService
{
    private readonly ICryptographyService _cryptographyService;
    private readonly byte[] _salt;
    
    public SecurityService(ICryptographyService cryptographyService)
    {
        _cryptographyService = cryptographyService;
        _salt = Encoding.ASCII.GetBytes("ASdjasd13jadsxASeaweCSA911wdasdXAesdo1204");
    }
    
    public byte[] Hash(string? plainText)
    {
        var bytes = Encoding.ASCII.GetBytes(plainText);
        return _cryptographyService.GenerateSaltedHash(bytes, _salt);
    }

    public string HashBase64(string? plainText)
    {
        var bytes = Hash(plainText);
        return Convert.ToBase64String(bytes);
    }

    public bool CompareHash(string plainText, byte[]? hashedPassword)
    {
        var bytesPlainText = Encoding.ASCII.GetBytes(plainText);
        return _cryptographyService.CompareByteArrays(bytesPlainText, hashedPassword);
    }

    public string? GetRandomHashedString()
    {
        var uniqueString =  Guid.NewGuid().ToString();
        var unixTime = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        var randomBytes = Encoding.ASCII.GetBytes(uniqueString + unixTime);
        var hashedBytes = _cryptographyService.GenerateSaltedHash(randomBytes, _salt);
        return Convert.ToBase64String(hashedBytes);
    }
    
    public string GetRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}