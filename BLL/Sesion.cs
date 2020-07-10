using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using BE;

namespace BLL
{
    public class Sesion
    {
        static Sesion _instancia;
        BLLUsuario bllUsuario;
        BLLRol bllRol;

        public Usuario conseguirUsuarioEnSesion(IIdentity user)
        {
            Usuario usuario = bllUsuario.conseguir(user.Name);
            usuario.Roles = bllRol.conseguir(usuario);
            return usuario;
        }

        protected Sesion()
        {
            bllUsuario = new BLLUsuario();
            bllRol = new BLLRol();
        }

        public static Sesion Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new Sesion();
            }
            return _instancia;
        }

        public void Eliminar()
        {
            _instancia = null;
        }

        public bool validar(Usuario usuario ,string codigo)
        {

            bool valido = false;
            foreach (Rol rol in usuario.Roles)
            {
                valido = busquedaRecursiva(rol, codigo);
                if (valido == true)
                {
                    break;
                }
            };
            return valido;
        }


        public void verificarPermiso(IIdentity user ,string codigo)
        {
            if (!user.IsAuthenticated)
            {
                throw new ForbiddenException();
            }

            Usuario usuario = conseguirUsuarioEnSesion(user);

            if (!validar(usuario ,codigo))
            {
                throw new ForbiddenException();
            }
        }

        public bool busquedaRecursiva(Rol rol, string codigo)
        {
            if (rol.Codigo == codigo)
            {
                return true;
            }

            if (rol is Familia)
            {
                foreach (Rol nuevoRol in rol.mostrar())
                {
                    if (busquedaRecursiva(nuevoRol, codigo) == true)
                    {
                        return true;
                    };
                };
            }
            return false;
        }


    }
}
