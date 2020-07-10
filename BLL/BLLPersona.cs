using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    class BLLPersona
    {
        BLLUsuario bllUsuario;
        DALPersona dalPersona;
        BLLBitacora bllBitacora;

        public BLLPersona()
        {
            dalPersona = new DALPersona();
            bllUsuario = new BLLUsuario();
            bllBitacora = new BLLBitacora();
        }


        /// <summary>
        /// conseguirTodos solicita a la DAL que retorne la lista de toods los Personas que existen en la base de datos.
        /// </summary>
        /// <returns></returns>
        public List<Persona> conseguirTodos()
        {
            return dalPersona.conseguirTodos();
        }


        /// <summary>
        /// calcularDVH se encarga de calcular el digito verificador horizontal en base a una entidad Persona.
        /// Para el calculo de DVH se realiza una concatenacion de los atributos del Persona, a su vez convirtiendo cada atributo que no sea string a tipo string
        /// Una vez realizada la concatenacion, es responsabilidad de la entidad Seguridad en retornar la encriptacion de dicha concatenacion.
        /// Una vez hecho eso, y obtenido el String de la concatenacion, por cada caracter, se obtiene el codigo ASCII que lo representa (int)
        /// y se suma cada codigo ASCII. El resultado es el Digito Verificador Horizontal
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
        public int calcularDVH(Persona persona)
        {
            string concatenacion = Convert.ToString(persona.Dni) + persona.Apellido + Convert.ToString(persona.Usuario.Id) + persona.Nombre + persona.Sexo + Convert.ToString(persona.Eliminado);
            Seguridad seguridad = new Seguridad();
            string md5 = seguridad.encriptar(concatenacion);


            //En este punto, verifico cada uno de los caracteres del MD5 obtenido, y los transformo al numero de codigo ASCII
            //multiplicado por su posicion en la cadena original
            byte[] asciiBytes = Encoding.ASCII.GetBytes(md5);

            int DVH = 0;
            int posicion = 0;

            foreach (byte b in asciiBytes)
            {
                posicion += 1;
                DVH = DVH + (int)b * posicion;
            }

            Console.WriteLine("DVH CONSEGUIDO = {0}", DVH);
            return DVH;

        }
    }
}
