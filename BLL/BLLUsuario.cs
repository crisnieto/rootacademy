using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class BLLUsuario : BLLBase
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
        /// Para el calculo de DVH se realiza una concatenacion de los atributos del nutricionista, a su vez convirtiendo cada atributo que no sea string a tipo string
        /// Una vez realizada la concatenacion, es responsabilidad de la entidad Seguridad en retornar la encriptacion de dicha concatenacion.
        /// Una vez hecho eso, y obtenido el String de la concatenacion, por cada caracter, se obtiene el codigo ASCII que lo representa (int)
        /// y se suma cada codigo ASCII. El resultado es el Digito Verificador Horizontal
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public int calcularDVH(Usuario usuario)
        {
            string concatenacion = usuario.Username + usuario.Password + Convert.ToString(usuario.Intentos) + Convert.ToString(usuario.Eliminado);
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
        /// 2. Se pasa a verificar si se encuentra bloqueado por exceso de intentos
        /// 3. Se valida que efectivamente el usuario conseguido de la base de datos y el ingresado, son iguales, a traves de la capa de seguridad
        /// 4. Se valida si tiene reintentos pero no este bloqueado, y se resetea a 0 en caso de que los tenga
        /// 
        /// 
        /// 1.1 Si no se encuentra el usuario, se catchea la excepcion y se muestra un error generico indicando que no se pudo loguear
        /// 2.1 Si se encuentra bloqueado, se muestra un mensaje indicando que el usuario se encuentra bloqueado
        /// 3.1 Si no se valida correctamente con la clase de seguridad, se informa con un mensaje de error generico indicando que no se pudo loguear

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
                if (!(seguridad.esBloqueado(usuarioConseguido)))
                {
                    if (seguridad.validar(usuarioConseguido, usuario))
                    {
                        usuarioConseguido.Roles = bllRol.conseguir(usuarioConseguido);
                        //Sesion.Instancia().UsuarioActual = usuarioConseguido;
                        bllBitacora.crearNuevaBitacora("Login de Usuario", "Se detecto un evento de ingreso", Criticidad.Media);
                    }
                    else
                    {
                        agregarIntento(usuarioConseguido);
                        bllBitacora.crearNuevaBitacora("Login de Usuario", "Se detecto un login incorrecto", Criticidad.Media);
                        throw new Exception(RootAcademyMessage.formatearMensaje("Login_messagebox_error_login"));
                    }

                }
                else
                {
                    bllBitacora.crearNuevaBitacora("Login de Usuario", "Se detecto un login incorrecto", Criticidad.Alta);
                    throw new Exception(RootAcademyMessage.formatearMensaje("El usuario se encuentra bloqueado por multiples intentos fallidos"));
                }

                if (usuarioConseguido.Intentos > 0)
                {
                    reiniciarIntentos(usuarioConseguido);
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
        /// actualizarPassword se encarga de encriptar la nueva password recibida por parametro a un Usuario Particular
        /// y solicitar a la DAL asociarla a un usuario especifico.
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int actualizarPassword(Usuario usuario, string password)
        {

            //verificarPermiso("OP100");

            try
            {
                if (password.Length > 0)
                {
                    usuario.Password = new Seguridad().encriptar(password);
                    Console.WriteLine("Nueva contraseña encriptada: " + usuario.Password);
                    usuario.Dvh = calcularDVH(usuario);
                    dalUsuario.actualizarContraseña(usuario);
                    bllBitacora.crearNuevaBitacora("Cambio de Password", "Se cambio la password del usuario " + usuario.Username, Criticidad.Media);
                    return new DVVH().actualizarDVV("Usuario");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                bllBitacora.crearNuevaBitacora("Cambio de Password", "Ocurrio un error en el cambio de Password " + ex.Message, Criticidad.Media);

                throw new Exception(RootAcademyMessage.formatearMensaje("MiCuenta_messagebox_error_cambio_password"), ex);
            }

        }


        /// <summary>
        /// Se encarga de actualizar la password al actual usuario.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// 
        /*
        public int actualizarPassword(string password)
        {
            try
            {
                if (password.Length > 0)
                {
                    Sesion.Instancia().UsuarioActual.Password = new Seguridad().encriptar(password);
                    Console.WriteLine("Nueva contraseña encriptada");
                    Sesion.Instancia().UsuarioActual.Dvh = calcularDVH(Sesion.Instancia().UsuarioActual);
                    dalUsuario.actualizarContraseña(Sesion.Instancia().UsuarioActual);
                    bllBitacora.crearNuevaBitacora("Cambio de Password", "Se cambio la password del usuario", Criticidad.Media);
                    return new DVVH().actualizarDVV("Usuario");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                bllBitacora.crearNuevaBitacora("Cambio de Password", "Ocurrio un error en el cambio de Password " + ex.Message, Criticidad.Media);

                throw new Exception(RootAcademyMessage.formatearMensaje("MiCuenta_messagebox_error_cambio_password"), ex);
            }

        }
        */


        /// <summary>
        /// crearUsuario se encarga de solicitar a la DAL la creacion de un nuevo usuario. Encriptando la password de la entidad usuario recibida previamente.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public int crearUsuario(Usuario usuario)
        {
            usuario.Password = new Seguridad().encriptar(usuario.Password);
            usuario.Dvh = calcularDVH(usuario);
            dalUsuario.ingresar(usuario);
            return new DVVH().actualizarDVV("Usuario");
        }

        /// <summary>
        /// eliminarUsuario se encarga de solicitar a la DAL la eliminacion de un usuario especifico. 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public int eliminarUsuario(Usuario usuario)
        {
            try
            {
                if (usuario.Username != "test")
                {
                    usuario.Eliminado = true;
                    usuario.Dvh = calcularDVH(usuario);
                    dalUsuario.eliminar(usuario);
                    int resultado = new DVVH().actualizarDVV("Usuario");
                    bllBitacora.crearNuevaBitacora("Eliminacion de Usuario", "Se elimino correctamente el usuario " + usuario.Username, Criticidad.Media);
                    return resultado;

                }
                return 0;
            }
            catch (Exception ex)
            {
                bllBitacora.crearNuevaBitacora("Eliminacion de Usuario", "Ocurrio un error al eliminar un usuario " + ex.Message, Criticidad.Media);

                throw ex;
            }
        }

        /// <summary>
        /// reiniciarIntetos se encarga de establecer la cantidad de intentos de un usuario especifico en 0.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool reiniciarIntentos(Usuario usuario)
        {
            try
            {
                usuario.Intentos = 0;
                usuario.Dvh = calcularDVH(usuario);
                dalUsuario.desbloquear(usuario);
                new DVVH().actualizarDVV("Usuario");
                bllBitacora.crearNuevaBitacora("Desbloqueo de Usuario", "Se desbloqueo el usuario " + usuario.Id, Criticidad.Media);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(RootAcademyMessage.formatearMensaje("Nutricionista_messagebox_reiniciar_intentos_usuario_error", e));
            }
        }


        /// <summary>
        /// agregarIntento se encarga de sumar un intento de login erroneo al Usuario recibido por parametro.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool agregarIntento(Usuario usuario)
        {
            //TODO: Hacer algo mas elegante aca...
            if (usuario.Username != "test")
            {
                usuario.Intentos += 1;
                usuario.Dvh = calcularDVH(usuario);
                dalUsuario.agregarIntento(usuario);
                new DVVH().actualizarDVV("Usuario");
                return true;
            }
            return false;
        }

        /// <summary>
        /// conseguirUsuarios se encarga de solicitar a la DAL la lista de todos los usuarios existentes en Base de Datos
        /// </summary>
        /// <returns></returns>
        public List<Usuario> conseguirUsuarios()
        {
            return dalUsuario.conseguirTodos();
        }

        /// <summary>
        /// conseguirUsuarios se encarga de solicitar a la DAL la lista de todos los usuarios existentes en Base de Datos. Incluye a los eliminados para realizar el calculo.
        /// </summary>
        /// <returns></returns>
        public List<Usuario> conseguirUsuariosValidacion()
        {
            return dalUsuario.conseguirTodosValidacion();
        }


        /// <summary>
        /// existe se encarga de validar si ya existe un usuario especifico o no.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool existe(Usuario usuario)
        {
            return dalUsuario.existe(usuario);
        }

        public Usuario GetUsuario(string username, string password)
        {
            Usuario usuario = new Usuario();
            usuario.Username = username;
            usuario.Password = password;
            return usuario;
        }

    }
}
