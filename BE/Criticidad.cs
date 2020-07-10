using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Criticidad
    {

        private Criticidad(string value) { Value = value; }

        public string Value { get; set; }

        public static Criticidad Alta { get { return new Criticidad("Alta"); } }
        public static Criticidad Media { get { return new Criticidad("Media"); } }
        public static Criticidad Baja { get { return new Criticidad("Baja"); } }
    }
}
