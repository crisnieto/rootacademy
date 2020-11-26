using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class SqlHelper
    {

        public void abrirConexion(SqlConnection conexion)
        {
            conexion.Open();
        }

        public void agregarParametro(SqlCommand comando, List<SqlParameter> lista)
        {
            for (int i = 0; i < lista.Count(); i++)
            {
                comando.Parameters.Add(lista[i]);
            }
        }

        public void cerrarConexion(SqlConnection sqlConexion)
        {
            sqlConexion.Close();
        }

        public SqlConnection conseguirStringConexion()
        {
            return new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=rootacademy;Integrated Security=True");
        }

        private int ejecutarComando(SqlCommand comando)
        {
            try
            {
                return comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(RootAcademyMessage.formatearMensaje("Database_messagebox_error_conexion"));
            }
        }


        public int ejecutarNonQuery(String textoComando, List<SqlParameter> lista = null)
        {
            SqlConnection conexion = conseguirStringConexion();
            SqlCommand comando = nuevoComando(textoComando, conexion);
            if (lista != null)
            {
                agregarParametro(comando, lista);
            }
            abrirConexion(conexion);
            ejecutarComando(comando);
            cerrarConexion(conexion);
            return 0;
        }



        public int ejecutarEscalar(string textoComando, List<SqlParameter> lista = null)
        {
            SqlConnection conexion = conseguirStringConexion();
            SqlCommand comando = nuevoComando(textoComando, conexion);
            if (lista != null)
            {
                agregarParametro(comando, lista);
            }
            abrirConexion(conexion);
            int id = ejecutarComandoEscalar(comando);
            cerrarConexion(conexion);
            return id;
        }

        private int ejecutarComandoEscalar(SqlCommand comando)
        {
            try
            {
                return (int)comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(RootAcademyMessage.formatearMensaje("Database_messagebox_error_conexion"));
            }
        }


        public DataSet ejecutarSelect(SqlCommand comando)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.SelectCommand = comando;
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            return dataSet;
        }

        public SqlCommand nuevoComando(string textoComando, SqlConnection conexion)
        {
            return new SqlCommand(textoComando, conexion);
        }

        public DataSet ejecutarDataAdapter(string textoComando, List<SqlParameter> lista = null)
        {
            SqlConnection conexion = conseguirStringConexion();
            SqlCommand comando = nuevoComando(textoComando, conexion);
            if (lista != null)
            {
                agregarParametro(comando, lista);
            }
            abrirConexion(conexion);
            DataSet data = ejecutarSelect(comando);
            cerrarConexion(conexion);
            return data;
        }

    }
}
