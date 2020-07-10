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
    public class DALPersona
    {
        SqlHelper sqlHelper;

        public DALPersona()
        {
            sqlHelper = new SqlHelper();
        }


        public List<Persona> conseguirTodos()
        {
            string textoComando = "SELECT * FROM PERSONA";
            DataTable dt = sqlHelper.ejecutarDataAdapter(textoComando).Tables[0];

            List<Persona> listaPersonas = new List<Persona>();

            foreach (DataRow dr in dt.Rows)
            {
                Persona persona = new Persona();
                persona.Nombre = (string)dr["nombre"];
                persona.Apellido = (string)dr["apellido"];
                persona.Id = (int)dr["personaID"];
                persona.Usuario.Id = (int)dr["usuarioId"];
                persona.Dni = (int)dr["dni"];
                persona.Sexo = (string)dr["sexo"];
                persona.Dvh = (int)dr["DVH"];
                persona.Eliminado = (bool)dr["eliminado"];
                listaPersonas.Add(persona);
            }
            return listaPersonas;
        }

        public Persona conseguir(int id)
        {
            string textoComando = "select dni, personaID, apellido, usuarioID, nombre, sexo, DVH from PERSONA " +
                "where usuarioID = @IDUSUARIO;";

            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@IDUSUARIO", id));

            DataRow dr = sqlHelper.ejecutarDataAdapter(textoComando, lista).Tables[0].Rows[0];

            Persona persona = new Persona();
            persona.Nombre = (string)dr["nombre"];
            persona.Apellido = (string)dr["apellido"];
            persona.Id = (int)dr["personaID"];
            persona.Usuario.Id = (int)dr["usuarioId"];
            persona.Dni = (int)dr["dni"];
            persona.Sexo = (string)dr["sexo"];
            persona.Dvh = (int)dr["DVH"];

            return persona;
        }
    }
}
