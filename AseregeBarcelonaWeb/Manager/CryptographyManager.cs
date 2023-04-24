using System;
using System.Security.Cryptography;
using System.Text;

namespace AseregeBarcelonaWeb.Manager
{
    public class CryptographyManager
    {
        //generar el sistema de cifrado
        public static string GeneratePasswordHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input); //los bytes del texto, estan representados en la codificación UTF-8
                byte[] hashBytes = sha256.ComputeHash(inputBytes); //el texto, se convierte en la clave segura que se guardara en la base de datos

                StringBuilder builder = new StringBuilder(); //convierte el hash anterior (binario) a un texto
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2")); // por cada ciclo, añade cada letra al "ArrayList"
                    //el 2 es el espacio
                }
                return builder.ToString(); //convertimos a un texto normal
            }
        }
        public static string GenerateHash(string seed, string data)
        {
            string seedAndData = seed + data;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(seedAndData));
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hash;
            }
        }
    }
}
