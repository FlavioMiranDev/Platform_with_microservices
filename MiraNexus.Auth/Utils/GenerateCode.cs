using System.Security.Cryptography;

namespace MiraNexus.Auth.Utils;

public static class GenerateCode
{
    public static string Generate()
    {
        var rng = RandomNumberGenerator.Create();
        byte[] randomBytes = new byte[4];
        rng.GetBytes(randomBytes);

        int number = Math.Abs(BitConverter.ToInt32(randomBytes, 0)) % 1000000;
        return number.ToString("D6");
    }
}
