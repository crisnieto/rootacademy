using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALBitacora
    {
        SqlHelper sqlHelper;

        public DALBitacora()
        {
            sqlHelper = new SqlHelper();
        }

        public bool borrar()
        {
            return true;
        }

        public int guardarBitacora(Bitacora bitacora)
        {
            string textoComando = "insert into bitacora (actividad, fecha, mensaje, tipoCriticidad, usuarioID) values (@ACTIVIDAD, @FECHA, @MENSAJE, @CRITICIDAD, @USUARIO)";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@ACTIVIDAD", bitacora.Actividad));
            lista.Add(new SqlParameter("@MENSAJE", bitacora.Descripción));
            lista.Add(new SqlParameter("@CRITICIDAD", bitacora.TipoCriticidad));
            if (bitacora.Usuario != null)
            {
                lista.Add(new SqlParameter("@USUARIO", bitacora.Usuario.Id));
            }
            else
            {
                lista.Add(new SqlParameter("@USUARIO", DBNull.Value));
            }
            SqlParameter parametroFecha = new SqlParameter("@FECHA", SqlDbType.DateTime);
            parametroFecha.Value = bitacora.Fecha;
            lista.Add(parametroFecha);

            return sqlHelper.ejecutarNonQuery(textoComando, lista);
        }

        public List<Bitacora> conseguirBitacorasConUsuario(Usuario usuario, DateTime fechaInicio, DateTime fechaFin, string criticidad = null)
        {
            string textoComando = "SELECT A.username, b.actividad, b.fecha, b.mensaje, b.tipoCriticidad, b.bitacoraID " +
            "FROM Usuario a JOIN Bitacora b ON(a.usuarioID = b.usuarioID) " +
            "WHERE a.usuarioID = @IDUSUARIO AND fecha >= @FECHAINICIO AND fecha <= @FECHAFIN";

            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@IDUSUARIO", usuario.Id));
            lista.Add(new SqlParameter("@FECHAINICIO", fechaInicio));
            lista.Add(new SqlParameter("@FECHAFIN", fechaFin));

            if (criticidad != null)
            {
                textoComando += " AND b.tipoCriticidad = @CRITICIDAD";
                lista.Add(new SqlParameter("@CRITICIDAD", criticidad));
            }

            textoComando += " ORDER BY b.fecha DESC";


            DataTable dt = sqlHelper.ejecutarDataAdapter(textoComando, lista).Tables[0];

            List<Bitacora> listaBitacora = new List<Bitacora>();
            foreach (DataRow row in dt.Rows)
            {
                Bitacora bitacora = new Bitacora();
                Usuario usuarioDeBitacora = new Usuario();
                bitacora.Actividad = (string)row["actividad"];
                bitacora.Descripción = (string)row["mensaje"];
                bitacora.TipoCriticidad = (string)row["tipoCriticidad"];
                bitacora.Fecha = (DateTime)row["fecha"];
                bitacora.Usuario = usuario;
                listaBitacora.Add(bitacora);
            }
            return listaBitacora;
        }

        public List<Bitacora> conseguirBitacorasSinUsuario(DateTime fechaInicio, DateTime fechaFin, string criticidad = null)
        {
            string textoComando = "SELECT A.username, b.actividad, b.fecha, b.mensaje, b.tipoCriticidad, b.bitacoraID " +
            "FROM Usuario a RIGHT JOIN Bitacora b ON(a.usuarioID = b.usuarioID) " +
            "WHERE fecha >= @FECHAINICIO AND fecha <= @FECHAFIN";

            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@FECHAINICIO", fechaInicio));
            lista.Add(new SqlParameter("@FECHAFIN", fechaFin));

            if (criticidad != null)
            {
                textoComando += " AND b.tipoCriticidad = @CRITICIDAD";
                lista.Add(new SqlParameter("@CRITICIDAD", criticidad));
            }

            textoComando += " ORDER BY b.fecha DESC";


            DataTable dt = sqlHelper.ejecutarDataAdapter(textoComando, lista).Tables[0];

            List<Bitacora> listaBitacora = new List<Bitacora>();
            foreach (DataRow row in dt.Rows)
            {
                Bitacora bitacora = new Bitacora();
                Usuario usuarioDeBitacora = new Usuario();
                bitacora.Actividad = (string)row["actividad"];
                bitacora.Descripción = (string)row["mensaje"];
                bitacora.TipoCriticidad = (string)row["tipoCriticidad"];
                bitacora.Fecha = (DateTime)row["fecha"];
                if (row["username"] != DBNull.Value)
                {
                    usuarioDeBitacora.Username = (string)row["username"];
                }
                bitacora.Usuario = usuarioDeBitacora;
                listaBitacora.Add(bitacora);
            }
            return listaBitacora;
        }


        public List<Usuario> conseguirUsuarios()
        {
            //Acá devuelvo directamente los usuarios que poseen bitacoras ya que es más
            //sencillo que estar devolviendo bitacoras con usuarios adentro,
            //solo me interesa saber los usuarios asociados a bitacoras para
            //consultarlos más adelante

            string textoComando = "SELECT DISTINCT b.usuarioID, A.username " +
            "FROM Usuario a JOIN Bitacora b ON(a.usuarioID = b.usuarioID)";

            DataTable dt = sqlHelper.ejecutarDataAdapter(textoComando).Tables[0];

            List<Usuario> listaUsuariosConBitacora = new List<Usuario>();
            foreach (DataRow row in dt.Rows)
            {
                Usuario usuarioDeBitacora = new Usuario();
                usuarioDeBitacora.Username = (string)row["username"];
                usuarioDeBitacora.Id = (int)row["usuarioID"];
                listaUsuariosConBitacora.Add(usuarioDeBitacora);
            }

            return listaUsuariosConBitacora;
        }

        public int seleccionarPorTipo()
        {
            return 0;
        }
    }
}
