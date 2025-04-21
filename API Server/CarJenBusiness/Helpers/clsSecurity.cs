using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.Helpers
{
    public class clsSecurity
    {

        // Hashing Using "SHA256"
        public static string ComputeHash(string Data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                Byte[] Bits = sha256.ComputeHash(Encoding.UTF8.GetBytes(Data));

                return BitConverter.ToString(Bits).Replace("-", "").ToLower();
            }
        }


        // Symmetric Encryption Using "Aes"
        public static string Encrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES encryption
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create an encryptor
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


                // Encrypt the data
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }


                    // Return the encrypted data as a Base64-encoded string
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
        public static string Decrypt(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES decryption
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create a decryptor
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);


                // Decrypt the data
                using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                {
                    // Read the decrypted data from the StreamReader
                    return srDecrypt.ReadToEnd();
                }
            }
        }


        // Asymmetric Encryption Using "RSACryptoServiceProvider"
        public static string RSA_Encrypt(string plainText, string publicKey)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {

                    // We use "rsa.ToXmlString(false)" to create a public key 

                    rsa.FromXmlString(publicKey);

                    byte[] encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), false);
                    return Convert.ToBase64String(encryptedData);
                }
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine($"Encryption error: {ex.Message}");
                throw; // Rethrow the exception to be caught in the Main method
            }
        }
        public static string RSA_Decrypt(string cipherText, string privateKey)
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    // We use "rsa.ToXmlString(true)" to create a private key 

                    rsa.FromXmlString(privateKey);


                    byte[] encryptedData = Convert.FromBase64String(cipherText);
                    byte[] decryptedData = rsa.Decrypt(encryptedData, false);


                    return Encoding.UTF8.GetString(decryptedData);
                }
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine($"Decryption error: {ex.Message}");
                throw; // Rethrow the exception to be caught in the Main method
            }
        }
    }
}
