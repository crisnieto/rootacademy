using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Persona
    {
        int id;
        int dni;
        string nombre;
        string apellido;
        string sexo;
        Usuario usuario;
        int dvh;
        bool eliminado;

        public Persona()
        {
            usuario = new Usuario();
        }

        public int Dni { get => dni; set => dni = value; }


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

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Apellido
        {
            get
            {
                return apellido;
            }

            set
            {
                apellido = value;
            }
        }

        public Usuario Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        public string Sexo
        {
            get
            {
                return sexo;
            }

            set
            {
                sexo = value;
            }
        }

        public int Dvh
        {
            get
            {
                return dvh;
            }

            set
            {
                dvh = value;
            }
        }

        public bool Eliminado
        {
            get
            {
                return eliminado;
            }

            set
            {
                eliminado = value;
            }
        }


        public override string ToString()
        {
            return this.Nombre + " " + this.Apellido;
        }
    }
}