namespace lego_indexor_api.Core.Interfaces.Services;

public interface ICryptographyService
{
    public byte[] GenerateSaltedHash(byte[] plainText, byte[] salt);
    public bool CompareByteArrays(byte[]? array1, byte[]? array2);
}