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
    public class DALUsuario
    {
        SqlHelper sqlHelper;

        public DALUsuario()
        {
            sqlHelper = new SqlHelper();
        }

        public Usuario conseguir(string usuario)
        {
            string textoComando = "SELECT * FROM USUARIO WHERE USERNAME = @USER and ELIMINADO = 0";

            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@USER", usuario));

            DataRow usuarioDS = sqlHelper.ejecutarDataAdapter(textoComando, lista).Tables[0].Rows[0];

            Usuario usuarioConseguido = new Usuario();

            usuarioConseguido.Id = (int)(usuarioDS["usuarioID"]);

            usuarioConseguido.Username = Convert.ToString(usuarioDS["username"]);

            usuarioConseguido.Password = Convert.ToString(usuarioDS["password"]);

            usuarioConseguido.Dvh = Convert.ToInt32(usuarioDS["DVH"]);

            usuarioConseguido.Intentos = Convert.ToInt32(usuarioDS["intentos"]);

            usuarioConseguido.Eliminado = Convert.ToBoolean(usuarioDS["eliminado"]);

            return usuarioConseguido;
        }

        public bool existe(Usuario usuario)
        {
            string textoComando = "SELECT * FROM USUARIO WHERE USERNAME = @USER";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@USER", usuario.Username));
            DataTable usuarioDt = sqlHelper.ejecutarDataAdapter(textoComando, lista).Tables[0];
            if (usuarioDt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<Usuario> conseguirTodos()
        {
            string textoComando = "SELECT usuarioiD, username, password, intentos, DVH, eliminado FROM USUARIO where eliminado = 0";

            DataTable usuarioDt = sqlHelper.ejecutarDataAdapter(textoComando).Tables[0];
            List<Usuario> listaUsuarios = new List<Usuario>();

            foreach (DataRow dr in usuarioDt.Rows)
            {
                Usuario usuarioConseguido = new Usuario();
                usuarioConseguido.Id = (int)(dr["usuarioID"]);
                usuarioConseguido.Username = Convert.ToString(dr["username"]);
                usuarioConseguido.Password = Convert.ToString(dr["password"]);
                usuarioConseguido.Dvh = Convert.ToInt32(dr["DVH"]);
                usuarioConseguido.Intentos = Convert.ToInt32(dr["intentos"]);
                usuarioConseguido.Eliminado = Convert.ToBoolean(dr["eliminado"]);
                listaUsuarios.Add(usuarioConseguido);
            }
            return listaUsuarios;
        }

        public List<Usuario> conseguirTodosValidacion()
        {
            string textoComando = "SELECT usuarioiD, username, password, intentos, DVH, eliminado FROM USUARIO";

            DataTable usuarioDt = sqlHelper.ejecutarDataAdapter(textoComando).Tables[0];
            List<Usuario> listaUsuarios = new List<Usuario>();

            foreach (DataRow dr in usuarioDt.Rows)
            {
                Usuario usuarioConseguido = new Usuario();
                usuarioConseguido.Id = (int)(dr["usuarioID"]);
                usuarioConseguido.Username = Convert.ToString(dr["username"]);
                usuarioConseguido.Password = Convert.ToString(dr["password"]);
                usuarioConseguido.Dvh = Convert.ToInt32(dr["DVH"]);
                usuarioConseguido.Intentos = Convert.ToInt32(dr["intentos"]);
                usuarioConseguido.Eliminado = Convert.ToBoolean(dr["eliminado"]);
                listaUsuarios.Add(usuarioConseguido);
            }
            return listaUsuarios;
        }


        public int eliminar(Usuario usuario)
        {
            string textoComando = "update USUARIO set eliminado = @ELIMINADO, DVH = @DVH where usuarioID = @USUARIOID";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@ELIMINADO", usuario.Eliminado));
            lista.Add(new SqlParameter("@DVH", usuario.Dvh));
            lista.Add(new SqlParameter("@USUARIOID", usuario.Id));
            return sqlHelper.ejecutarNonQuery(textoComando, lista);
        }

        public int ingresar(Usuario usuario)
        {
            string textoComando = "insert into USUARIO (username, password, DVH) values (@USERNAME, @PASSWORD, @DVH)";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@PASSWORD", usuario.Password));
            lista.Add(new SqlParameter("@USERNAME", usuario.Username));
            lista.Add(new SqlParameter("@DVH", usuario.Dvh));
            return sqlHelper.ejecutarNonQuery(textoComando, lista);

        }

        public int actualizarContraseña(Usuario usuario)
        {
            string textoComando = "UPDATE Usuario set password = @PASSWORD, dvh = @DVH WHERE UsuarioID = @USUARIOID;";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@PASSWORD", usuario.Password));
            lista.Add(new SqlParameter("@DVH", usuario.Dvh));
            lista.Add(new SqlParameter("@USUARIOID", usuario.Id));

            return sqlHelper.ejecutarNonQuery(textoComando, lista);
        }

        public bool guardar(Usuario usuario)
        {
            string textoComando = "INSERT INTO Usuario (username, password, DVH) values @USER, @PASSWORD, @DVH;";

            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@USER", usuario.Username));
            lista.Add(new SqlParameter("@PASSWORD", usuario.Password));
            lista.Add(new SqlParameter("@DVH", usuario.Dvh));

            return Convert.ToBoolean(sqlHelper.ejecutarNonQuery(textoComando, lista));

        }

        public bool agregarIntento(Usuario usuario)
        {
            string textoComando = "UPDATE Usuario SET intentos = @INTENTOS, DVH = @DVH where username = @USER";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@USER", usuario.Username));
            lista.Add(new SqlParameter("@DVH", usuario.Dvh));
            lista.Add(new SqlParameter("@INTENTOS", usuario.Intentos));
            return Convert.ToBoolean(sqlHelper.ejecutarNonQuery(textoComando, lista));
        }

        public bool desbloquear(Usuario usuario)
        {
            string textoComando = "UPDATE Usuario SET intentos = @INTENTOS, DVH = @DVH where username = @USER";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@USER", usuario.Username));
            lista.Add(new SqlParameter("@INTENTOS", usuario.Intentos));
            lista.Add(new SqlParameter("@DVH", usuario.Dvh));

            return Convert.ToBoolean(sqlHelper.ejecutarNonQuery(textoComando, lista));
        }
    }

}
