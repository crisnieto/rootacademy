using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DVVH
    {
        SqlHelper sqlHelper;

        public DVVH()
        {
            sqlHelper = new SqlHelper();
        }

        public int conseguirDVV(string tabla)
        {
            string textoComando = "SELECT valor FROM DVV WHERE nombreTabla=@TABLA";

            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@TABLA", tabla));

            DataSet dvvDS = sqlHelper.ejecutarDataAdapter(textoComando, lista);
            return (int)dvvDS.Tables[0].Rows[0]["valor"];
        }



        public List<int> obtenerListaDVHdeTabla(string tabla)
        {
            string textoComando = "SELECT DVH FROM " + tabla;
            DataSet dvhDS = sqlHelper.ejecutarDataAdapter(textoComando);

            List<int> listaDVH = new List<int>();

            foreach (DataRow row in dvhDS.Tables[0].Rows)
            {
                listaDVH.Add((int)row["DVH"]);
            }

            return listaDVH;
        }

        public int actualizarDVV(string tabla)
        {
            string textoComando = "SELECT DVH FROM " + tabla;
            DataTable dt = sqlHelper.ejecutarDataAdapter(textoComando).Tables[0];
            int suma = 0;
            foreach (DataRow dr in dt.Rows)
            {
                suma += (int)dr["DVH"];
            }

            string textoComando2 = "UPDATE DVV SET valor = @SUMA WHERE nombreTabla = @TABLA";
            List<SqlParameter> lista2 = new List<SqlParameter>();
            lista2.Add(new SqlParameter("@SUMA", suma));
            lista2.Add(new SqlParameter("@TABLA", tabla));
            return sqlHelper.ejecutarNonQuery(textoComando2, lista2);
        }
    }
}
