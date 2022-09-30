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

    public bool CompareHash(string plainText, byte[]? hashedPassword)
    {
        var bytesPlainText = Encoding.ASCII.GetBytes(plainText);
        return _cryptographyService.CompareByteArrays(bytesPlainText, hashedPassword);
    }
}