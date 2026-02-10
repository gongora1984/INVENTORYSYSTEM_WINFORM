using System.Security.Cryptography;
using System.Text;

namespace InventorySystem.WinForms.Services;

public static class SecurityService
{
    // A fixed key for internal app use. In a highly secure environment, this would be managed differently,
    // but for scrambling a config file for distribution, this is effective.
    private static readonly string InternalKey = "InvSys_Secret_Key_2024_@!"; 

    public static string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return plainText;

        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = GetKeyBytes();
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    public static string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return cipherText;

        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = GetKeyBytes();
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }

    private static byte[] GetKeyBytes()
    {
        // Ensure the key is exactly 32 bytes for AES-256
        byte[] keyBytes = Encoding.UTF8.GetBytes(InternalKey);
        byte[] finalKey = new byte[32];
        Array.Copy(keyBytes, finalKey, Math.Min(keyBytes.Length, 32));
        return finalKey;
    }
}
