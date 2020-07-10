using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BLLUsuario
    {
        public BLLRol bllRol;
        public DALUsuario dalUsuario;
        BLLBitacora bllBitacora;

        public BLLUsuario()
        {
            bllRol = new BLLRol();
            dalUsuario = new DALUsuario();
            bllBitacora = new BLLBitacora();
        }

        /// <summary>
        /// calcularDVH se encarga de calcular el digito verificador horizontal en base a una entidad Usuario.
        /// Para el calculo de DVH se realiza una concatenacion de los atributos del Usuario, a su vez convirtiendo cada atributo que no sea string a tipo string
        /// Una vez realizada la concatenacion, es responsabilidad de la entidad Seguridad en retornar la encriptacion de dicha concatenacion.
        /// Una vez hecho eso, y obtenido el String de la concatenacion, por cada caracter, se obtiene el codigo ASCII que lo representa (int)
        /// y se suma cada codigo ASCII. El resultado es el Digito Verificador Horizontal
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public int calcularDVH(Usuario usuario)
        {
            string concatenacion = usuario.Username + usuario.Password + Convert.ToString(usuario.Eliminado);
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


        /// <summary>
        /// 1. Se busca el usuario en la base de datos
        /// 2. Se valida que efectivamente el usuario conseguido de la base de datos y el ingresado, son iguales, a traves de la capa de seguridad
        /// 
        /// 
        /// 1.1 Si no se encuentra el usuario, se catchea la excepcion y se muestra un error generico indicando que no se pudo loguear
        /// 2.1 Si no se valida correctamente con la clase de seguridad, se informa con un mensaje de error generico indicando que no se pudo loguear

        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario conseguirUsuarioLogIn(Usuario usuario)
        {
            Seguridad seguridad = new Seguridad();
            Usuario usuarioConseguido;

            usuario.Password = seguridad.encriptar(usuario.Password);

            try
            {
                usuarioConseguido = dalUsuario.conseguir(usuario.Username);
            }
            catch (Exception exception)
            {
                Console.Write(exception);
                throw new Exception(RootAcademyMessage.formatearMensaje("Error al efectuar el login. Verifique los datos"));
            }
            try
            {
                if (seguridad.validar(usuarioConseguido, usuario))
                {
                    usuarioConseguido.Roles = bllRol.conseguir(usuarioConseguido);
                    bllBitacora.crearNuevaBitacora("Login de Usuario", "Se detecto un evento de ingreso", Criticidad.Media);
                }
                else
                {
                    bllBitacora.crearNuevaBitacora("Login de Usuario", "Se detecto un login incorrecto", Criticidad.Media);
                    throw new Exception(RootAcademyMessage.formatearMensaje("Login_messagebox_error_login"));
                }
                return usuarioConseguido;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                bllBitacora.crearNuevaBitacora("Login de Usuario", "Ocurrio un error en el login de usuario " + ex.Message, Criticidad.Media);
                throw new Exception(RootAcademyMessage.formatearMensaje(ex.Message));
            }
        }

        /// <summary>
        /// conseguir solicita a la DAL obtener un usuario especifico de la base de datos.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Usuario conseguir(string usuario)
        {
            try
            {
                return dalUsuario.conseguir(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(RootAcademyMessage.formatearMensaje("messagebox_usuario_inexistente"));
            }
        }

        /// <summary>
        /// conseguirUsuarios se encarga de solicitar a la DAL la lista de todos los usuarios existentes en Base de Datos. Incluye a los eliminados para realizar el calculo.
        /// </summary>
        /// <returns></returns>
        public List<Usuario> conseguirUsuariosValidacion()
        {
            return dalUsuario.conseguirTodosValidacion();
        }


        public Usuario construirUsuarioRecibido(string username, string password)
        {
            Usuario usuario = new Usuario();
            usuario.Username = username;
            usuario.Password = password;
            return usuario;
        }

    }
}
