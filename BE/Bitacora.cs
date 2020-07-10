using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Bitacora
    {
        string actividad;
        string descripción;
        DateTime fecha;
        string tipoCriticidad;
        Usuario usuario;

        public string Actividad
        {
            get
            {
                return actividad;
            }

            set
            {
                actividad = value;
            }
        }

        public string Descripción
        {
            get
            {
                return descripción;
            }

            set
            {
                descripción = value;
            }
        }

        public DateTime Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
            }
        }

        public string TipoCriticidad
        {
            get
            {
                return tipoCriticidad;
            }

            set
            {
                tipoCriticidad = value;
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
    }
}
