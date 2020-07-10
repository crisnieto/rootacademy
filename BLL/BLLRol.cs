using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BLLRol : BLLBase
    {

        DALRol dalRol;
        BLLBitacora bllBitacora;

        public BLLRol()
        {
            dalRol = new DALRol();
            bllBitacora = new BLLBitacora();
        }

        /// <summary>
        /// Consigue la lista de Roles de un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<Rol> conseguir(Usuario usuario)
        {
            List<Rol> roles = dalRol.conseguirRolesDeUsuario(usuario);
            return roles;
        }

        /// <summary>
        /// Consigue la lista de Roles global
        /// </summary>
        /// <returns></returns>
        public List<Rol> conseguir()
        {
            //verificarPermiso("AA099");

            List<Rol> roles = dalRol.conseguirRoles();
            return roles;
        }

 
        public void insertar()
        {

        }


        public bool validarCodigoDeRol(string codigo)
        {
            if (codigo.Length > 0)
            {
                return dalRol.validarCodigoDeRol(codigo);
            }
            return false;
        }

        /// <summary>
        /// Se encarga de crear un rol nuevo en la base de datos. Puede tambien tener un padre con lo que se trataria de asociar un nuevo permiso a una familia.
        /// </summary>
        /// <param name="rol"></param>
        /// <param name="padre"></param>
        /// <returns></returns>
        public bool crearRol(Rol rol, Rol padre = null)
        {
            if (rol.Codigo.Length == 0 || rol.Descripcion.Length == 0)
            {
                throw new Exception(RootAcademyMessage.formatearMensaje("GestionRoles_messagebox_error_creacion"));
            }
            try
            {
                //verificarPermiso("AA099");
                bool result = dalRol.crearRol(rol, padre);
                bllBitacora.crearNuevaBitacora("Creacion de Rol", "Se creo el rol " + rol.Codigo, Criticidad.Alta);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(RootAcademyMessage.formatearMensaje("GestionRoles_messagebox_error_creacion", ex));
            }

        }

        /// <summary>
        /// Se encarga de asociar un Rol a un usuario en particular.
        /// </summary>
        /// <param name="rol"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool asociarAUsuario(Rol rol, Usuario usuario)
        {
            //verificarPermiso("AA099");


            if (esPosibleAsociarRol(rol, usuario.Roles))
            {
                bllBitacora.crearNuevaBitacora("Asociacion de Rol", "Se asocio el rol " + rol.Codigo + " del usuario " + usuario.Username, Criticidad.Media);

                return dalRol.asociarRolAUsuario(rol, usuario);
            }
            else
            {
                new BLLBitacora().crearNuevaBitacora("Asociacion de Rol", "Error al asociar el rol " + rol.Codigo + " del usuario " + usuario.Username, Criticidad.Baja);

                throw new Exception(RootAcademyMessage.formatearMensaje("GestionRoles_messagebox_error_asociacion"));
            }
        }

        /// <summary>
        /// Verifica si ya el rol ya existe en una lista de roles previa.
        /// </summary>
        /// <param name="rolBuscado"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool esPosibleAsociarRol(Rol rolBuscado, List<Rol> roles)
        {
            return !contieneElRol(rolBuscado, roles);
        }


        /// <summary>
        /// Busca recursivamente si ya existe el rol en la lista de roles previamente recibida.
        /// </summary>
        /// <param name="rolBuscado"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool contieneElRol(Rol rolBuscado, List<Rol> roles)
        {
            bool resultado = false;
            foreach (Rol rolHijo in roles)
            {
                if (rolHijo.Codigo == rolBuscado.Codigo)
                {
                    return true;
                }
                if (rolHijo is Familia)
                {
                    resultado = contieneElRol(rolBuscado, ((Familia)rolHijo).Roles);
                    if (resultado == true)
                    {
                        return resultado;
                    }
                }
            }
            return resultado;

        }

    }
}
