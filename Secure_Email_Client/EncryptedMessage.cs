using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1.X9;
using MimeKit;

namespace Secure_Email_Client
{
    public static class EncryptedMessage
    {
        private static readonly string jsonFilePath = ".rsa";
        public static readonly string tempEmails = "temp_emails";

        public static (string pub, string priv) GenerateRSAKeys()
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                string publicKey = rsa.ToXmlString(false);
                string privateKey = rsa.ToXmlString(true);

                return (publicKey, privateKey);
            }
        }

        public static (byte[] key, byte[] iv) GenerateDESKeyIV()
        {
            using (var des = DES.Create())
            {
                byte[] key = des.Key;
                byte[] iv = des.IV;

                return (key, iv);
            }
        }

        public static (string keyPath, string ivPath) EncryptDES(string user, string userfrom, byte[] key, byte[] iv)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                string rsaPublicKey = GetUserKeyFromJsonFile(user, userfrom, "public");

                rsa.FromXmlString(rsaPublicKey);

                byte[] encryptedKey = rsa.Encrypt(key, false);
                byte[] encryptedIV = iv;

                string keyPath = Path.Combine(tempEmails, user, user + "_des_key_temp");
                string ivPath = Path.Combine(tempEmails, user, user + "_des_iv_temp");

                if (!Directory.Exists(Path.Combine(tempEmails, user)))
                {
                    Directory.CreateDirectory(Path.Combine(tempEmails, user));
                }

                File.WriteAllBytes(keyPath, encryptedKey);
                File.WriteAllBytes(ivPath, encryptedIV);

                return (keyPath, ivPath);
            }
        }

        public static (byte[] key, byte[] iv) DecryptDES(string user, string userfrom, byte[] key, byte[] iv)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                string rsaPrivateKey = GetUserKeyFromJsonFile(user, userfrom, "private");

                rsa.FromXmlString(rsaPrivateKey);

                byte[] encryptedKey = rsa.Decrypt(key, false);
                byte[] encryptedIV = iv;

                return (encryptedKey, encryptedIV);
            }
        }

        public static void AddKeysToJsonFile(string userName, string publicKey, string privateKey)
        {
            // Создаем объект, который будет добавлен в JSON-файл
            var currentUser = new Dictionary<string, string>
            {
               { "username", userName },
               { "public", publicKey },
               { "private", privateKey }
            };

            string jsonData;
            var users = new List<Dictionary<string, string>>();

            var path = Path.Combine("emails", userName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            try
            {
                // Читаем существующий JSON-файл
                jsonData = File.ReadAllText(Path.Combine(path, jsonFilePath));
                users = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
            }
            catch
            {

            }

            // Добавляем нового пользователя в список пользователей
            users?.Add(currentUser);

            // Записываем обновленный список пользователей обратно в JSON-файл
            jsonData = JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(Path.Combine(path, jsonFilePath), jsonData);
        }

        public static void GetAllUserFromJsonFile(ref List<RSAUsers> rsaUsers, string user)
        {
            try
            {
                string jsonData;
                var users = new List<Dictionary<string, string>>();

                var path = Path.Combine("emails", user);

                try
                {
                    jsonData = File.ReadAllText(Path.Combine(path, jsonFilePath));
                    users = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
                }
                catch
                {

                }

                foreach (var item in users)
                {
                    // Создаем объект, который будет добавлен в JSON-файл
                    var currentUser = new RSAUsers
                    {
                        userName = item["username"],
                        publicKey = item["public"],
                        privateKey = item["private"]
                    };

                    rsaUsers.Add(currentUser);
                }
            }
            catch (Exception exc)
            {
                return;
            }
        }

        public static void WriteAllUserFromJsonFile(List<RSAUsers> rsaUsers, string user)
        {
            try
            {
                List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

                foreach (var item in rsaUsers)
                {
                    // Создаем объект, который будет добавлен в JSON-файл
                    var currentUser = new Dictionary<string, string>
                    {
                        { "username", item.userName },
                        { "public", item.publicKey },
                        { "private", item.privateKey }
                    };

                    list.Add(currentUser);
                }

                var path = Path.Combine("emails", user);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Записываем обновленный список пользователей обратно в JSON-файл
                var jsonData = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(Path.Combine(path, jsonFilePath), jsonData);
            }
            catch (Exception exc)
            {
                return;
            }
        }

        private static string GetUserKeyFromJsonFile(string userName, string userPath, string keyName)
        {
            string jsonData;
            var users = new List<Dictionary<string, string>>();

            var path = Path.Combine("emails", userPath);

            try
            {
                jsonData = File.ReadAllText(Path.Combine(path, jsonFilePath));
                users = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonData);
            }
            catch
            {

            }

            // Ищем пользователя по имени
            var currentUser = users?.Find(u => u["username"] == userName);

            // Если пользователь найден, возвращаем соответствующий ключ
            if (currentUser != null)
            {
                return currentUser[keyName];
            }

            // Если пользователь не найден, возвращаем null
            return null;
        }

        public static string EncryptMessage(byte[] sourceData, string user, string userfrom, DateTime date, byte[] key, byte[] iv)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                using (var des = DES.Create())
                {
                    string rsaPublicKey = GetUserKeyFromJsonFile(user, userfrom, "public");

                    rsa.FromXmlString(rsaPublicKey);

                    des.Key = key;
                    des.IV = iv;

                    if (!Directory.Exists(Path.Combine(tempEmails, user)))
                    {
                        Directory.CreateDirectory(Path.Combine(tempEmails, user));
                    }

                    string sourcefile = Path.Combine(tempEmails, user, user + "_temp");
                    string outputfile = Path.Combine(tempEmails, user, user + "_enc_" + date.ToString().Replace(':', '.'));

                    using (var bw = new BinaryWriter(File.Open(sourcefile, FileMode.Create)))
                    {
                        bw.Write(sourceData);
                    }

                    EncryptFile(sourcefile, outputfile, des.Key, des.IV);

                    return outputfile;
                }
            }
        }

        public static byte[] DecryptMessage(string encryptedFile, byte[] desKey, byte[] desIV, string user, string userfrom)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                string rsaPrivateKey = GetUserKeyFromJsonFile(user, userfrom, "public");

                rsa.FromXmlString(rsaPrivateKey);

                byte[] decryptedDesKey = desKey;
                byte[] decryptedDesIV = desIV;

                byte[] array;

                using (var des = DES.Create())
                {
                    using (var stream = File.OpenRead(encryptedFile))
                    {
                        array = DecryptFile(stream, decryptedDesKey, decryptedDesIV);
                    }
                }

                return array;
            }
        }

        private static void EncryptFile(string inputFile, string outputFile, byte[] key, byte[] iv)
        {
            using (var des = DES.Create())
            using (var fsInput = new FileStream(inputFile, FileMode.Open))
            using (var fsEncrypted = new FileStream(outputFile, FileMode.Create))
            using (var encryptor = des.CreateEncryptor(key, iv))
            using (var cs = new CryptoStream(fsEncrypted, encryptor, CryptoStreamMode.Write))
            {
                byte[] buffer = new byte[1048576]; // Читаем по 1MB
                int read;

                while ((read = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                {
                    cs.Write(buffer, 0, read);
                }
            }

            if (File.Exists(inputFile))
            {
                File.Delete(inputFile);
            }
        }

        private static byte[] DecryptFile(Stream source, byte[] key, byte[] iv)
        {
            using (var des = DES.Create())
            using (var fsDecrypted = new MemoryStream())
            using (var decryptor = des.CreateDecryptor(key, iv))
            using (var cs = new CryptoStream(source, decryptor, CryptoStreamMode.Read))
            {
                byte[] buffer = new byte[1048576]; // Читаем по 1MB
                int read;

                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fsDecrypted.Write(buffer, 0, read);
                }

                return fsDecrypted.ToArray();
            }
        }

        public static void DeleteDirectory(string path)
        {
            foreach (var directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }

            foreach (var file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }

            Directory.Delete(path);
        }

        public static byte[] CreateSignature(byte[] data, string privateKey)
        {
            byte[] signature;

            using (var dsa = new DSACryptoServiceProvider())
            {
                dsa.FromXmlString(privateKey);
                signature = dsa.SignData(data, HashAlgorithmName.SHA1);
            }

            return signature;
        }

        public static bool VerifySignature(byte[] data, byte[] signature, string publicKey)
        {
            using (var dsa = new DSACryptoServiceProvider())
            {
                dsa.FromXmlString(publicKey);
                return dsa.VerifyData(data, signature, HashAlgorithmName.SHA1);
            }
        }
    }
}
