using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BLLRol
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
