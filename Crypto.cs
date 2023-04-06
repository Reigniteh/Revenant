using System;
using System.IO;
using System.Security.Cryptography;

namespace Phasmophobia_Save_Editor
{
  internal class Crypto
  {
    private const string PASSWORD = "t36gref9u84y7f43g";

    public static byte[] DecryptSaveData(byte[] data)
    {
      using (MemoryStream memoryStream = new MemoryStream(data))
      {
        using (MemoryStream destination = new MemoryStream())
        {
          byte[] numArray = new byte[16];
          memoryStream.Read(numArray, 0, 16);
          using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes("t36gref9u84y7f43g", numArray, 100))
          {
            using (Aes aes = Aes.Create())
            {
              aes.Mode = CipherMode.CBC;
              aes.Padding = PaddingMode.PKCS7;
              aes.Key = rfc2898DeriveBytes.GetBytes(16);
              aes.IV = numArray;
              using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                cryptoStream.CopyTo((Stream) destination);
            }
          }
          return destination.ToArray();
        }
      }
    }

    public static byte[] EncryptSaveData(byte[] data)
    {
      using (MemoryStream memoryStream1 = new MemoryStream(data))
      {
        using (MemoryStream memoryStream2 = new MemoryStream())
        {
          byte[] numArray = new byte[16];
          new Random().NextBytes(numArray);
          memoryStream2.Write(numArray, 0, 16);
          using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes("t36gref9u84y7f43g", numArray, 100))
          {
            using (Aes aes = Aes.Create())
            {
              aes.Mode = CipherMode.CBC;
              aes.Padding = PaddingMode.PKCS7;
              aes.Key = rfc2898DeriveBytes.GetBytes(16);
              aes.IV = numArray;
              using (CryptoStream destination = new CryptoStream((Stream) memoryStream2, aes.CreateEncryptor(), CryptoStreamMode.Write))
                memoryStream1.CopyTo((Stream) destination);
            }
          }
          return memoryStream2.ToArray();
        }
      }
    }
  }
}
