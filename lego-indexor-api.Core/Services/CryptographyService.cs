using System.Collections;
using System.Security.Cryptography;
using lego_indexor_api.Core.Interfaces.Services;

namespace lego_indexor_api.Core.Services;

public class CryptographyService : ICryptographyService
{
    public byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
    {
        HashAlgorithm algorithm = SHA256.Create();

        var plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

        for (var i = 0; i < plainText.Length; i++)
            plainTextWithSaltBytes[i] = plainText[i];
        
        for (var i = 0; i < salt.Length; i++)
            plainTextWithSaltBytes[plainText.Length + i] = salt[i];

        return algorithm.ComputeHash(plainTextWithSaltBytes);  
    }

    public bool CompareByteArrays(byte[]? array1, byte[]? array2)
    {
        if (array1 == null && array2 == null)
            return true;
        if (array1 == null || array2 == null)
            return false;
        return AreArraysSameLength(array1, array2) && AreArraysEqual(array1, array2);
    }

    private bool AreArraysSameLength(ICollection array1, ICollection array2)
    {
        return array1.Count == array2.Count;
    }

    private bool AreArraysEqual(IReadOnlyList<byte>? array1, IReadOnlyList<byte>? array2)
    {
        for (var i = 0; i < array1.Count; i++)
        {
            if (array1[i] != array2[i])
                return false;
        }
        return true;
    }
}