using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALBackupManager
    {
        SqlHelper sqlHelper;
        public DALBackupManager()
        {
            sqlHelper = new SqlHelper();
        }

        public int crearBackup(string path)
        {
            string textoComando1 = "USE master ALTER DATABASE rootacademy SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
            sqlHelper.ejecutarNonQuery(textoComando1);

            string textoComando2 = "ALTER DATABASE rootacademy SET MULTI_USER;";
            sqlHelper.ejecutarNonQuery(textoComando2);

            //Si hay un error de Access denied, verificar la configuración del servicio de SQL Server.
            //Debe estar configurado en Local System Account
            //Fuente: https://stackoverflow.com/a/35464963
            string textoComando3 = "USE master BACKUP DATABASE rootacademy TO DISK='" + path + "'";
            return sqlHelper.ejecutarNonQuery(textoComando3);
        }

        public int ejecutarRestore(string path)
        {
            //Las primeras dos queries son un workaround para matar cualquier otra conexión que esté viva (ejemplo: SSMS)
            //Fuente: https://stackoverflow.com/a/18699584
            string textoComando1 = "USE master ALTER DATABASE rootacademy SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
            sqlHelper.ejecutarNonQuery(textoComando1);

            string textoComando2 = "ALTER DATABASE rootacademy SET MULTI_USER;";
            sqlHelper.ejecutarNonQuery(textoComando2);

            string textoComando3 = "USE MASTER RESTORE DATABASE [rootacademy] FROM DISK= '" + path + "'" + "WITH REPLACE";
            return sqlHelper.ejecutarNonQuery(textoComando3);
        }
    }
}
