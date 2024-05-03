using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

namespace RPSGame.Tools;

public static class HMacSha3
{
    public static string CalculateHMACSHA3(byte[] data, byte[] key)
    {
        var digest = new Sha3Digest(256);
        var hmac = new HMac(digest);
        hmac.Init(new KeyParameter(key));

        hmac.BlockUpdate(data, 0, data.Length);
        byte[] result = new byte[hmac.GetMacSize()];
        hmac.DoFinal(result, 0);

        return BitConverter.ToString(result).Replace("-", "").ToLowerInvariant();
    }
}