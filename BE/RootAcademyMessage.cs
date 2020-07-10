using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class RootAcademyMessage
    {
        public static string formatearMensaje(string message)
        {
            return message;
        }

        public static string formatearMensaje(string message, Exception exception)
        {
            return message + " : " + exception.Message;
        }

    }
}
