using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class Rol
    {
        int id;
        string descripcion;
        string codigo;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public string Codigo
        {
            get
            {
                return codigo;
            }

            set
            {
                codigo = value;
            }
        }

        abstract public void agregar(Rol rol);

        abstract public void eliminar(Rol rol);

        abstract public List<Rol> mostrar();

        public override string ToString()
        {
            return this.Codigo + " - " + this.Descripcion;
        }

    }
}
