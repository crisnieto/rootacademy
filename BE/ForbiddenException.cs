using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ForbiddenException : Exception
    {

        public ForbiddenException()
        : base("No tiene permisos para acceder a esta sección")
        {
           
        }



    }
}
