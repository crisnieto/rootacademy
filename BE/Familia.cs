using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Familia : Rol
    {
        List<Rol> roles;

        public Familia()
        {
            Roles = new List<Rol>();
        }

        public List<Rol> Roles
        {
            get
            {
                return roles;
            }

            set
            {
                roles = value;
            }
        }

        public override void agregar(Rol rol)
        {
            Roles.Add(rol);
        }

        public override void eliminar(Rol rol)
        {
            Roles.Remove(rol);
        }

        public override List<Rol> mostrar()
        {
            return Roles;
        }

    }
}
