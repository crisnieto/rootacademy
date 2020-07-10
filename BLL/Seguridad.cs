using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    class Seguridad
    {
        /// <summary>
        /// encriptar convierte el dato entrante en un hash MD5 en formato Hexadecimal.
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        public string encriptar(string dato)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(dato));

            byte[] result = md5.Hash;

            StringBuilder hash = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                hash.Append(result[i].ToString("x2"));
            }
            return hash.ToString();
        }


        /// <summary>
        /// validar se encarga de validar que los atributos de usuario y password ingresados sean iguales a los guardados.
        /// </summary>
        /// <param name="usuarioConseguido"></param>
        /// <param name="usuarioIngresado"></param>
        /// <returns></returns>
        public bool validar(Usuario usuarioConseguido, Usuario usuarioIngresado)
        {
            if (usuarioConseguido.Username == usuarioIngresado.Username && usuarioConseguido.Password == usuarioIngresado.Password)
            {
                return true;
            }
            else
            {
                Console.WriteLine(usuarioConseguido.Username);
                Console.WriteLine(usuarioIngresado.Username);

                Console.WriteLine(usuarioConseguido.Password);
                Console.WriteLine(usuarioIngresado.Password);


                Console.WriteLine("Los datos ingresados no concuerdan");
                return false;
            }
        }
    }
}
