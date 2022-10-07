namespace lego_indexor_api.Core.Interfaces.Services;

public interface ISecurityService
{
    public byte[] Hash(string? plainText);
    public bool CompareHash(string plainText, byte[]? hashedPassword);
    public string GetRandomHashedString();
}