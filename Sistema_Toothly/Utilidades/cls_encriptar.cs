using System;
using System.Security.Cryptography;
using System.Text;

namespace Sistema_Toothly.Utilidades
{
    public class cls_encriptar
    {
        public const string Llave = "estrella";
        public const string ErrorDesencriptar = "Error al desencriptar la clave";

        private TripleDES DES = TripleDES.Create();

        private byte[] MD5Hash(string value)
        {
            return MD5.HashData(ASCIIEncoding.ASCII.GetBytes(value));
        }

        public string encriptar(string textoaCodificar, string key)
        {
            DES.Key = this.MD5Hash(key);
            DES.Mode = CipherMode.ECB;
            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(textoaCodificar);
            return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }

        public string desencriptar(string textoaDescodificar, string key)
        {
            try
            {
                DES.Key = this.MD5Hash(key);
                DES.Mode = CipherMode.ECB;
                byte[] buffer = Convert.FromBase64String(textoaDescodificar);
                return ASCIIEncoding.ASCII.GetString(DES.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception)
            {
                return ErrorDesencriptar;
            }
        }
    }
}
