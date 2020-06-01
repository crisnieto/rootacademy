using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLUsuario
    {

        public Usuario GetUsuario(string username, string password)
        {
            Usuario usuario = new Usuario();
            usuario.Username = username;
            usuario.Password = password;
            return usuario;
        }

    }
}
