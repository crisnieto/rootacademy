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
    public class DALRol
    {
        SqlHelper sqlHelper;

        public DALRol()
        {
            sqlHelper = new SqlHelper();
        }

        /// <summary>
        /// Consigue los roles de usuario interactuando con la base de datos por cada rol encontrado, se agrega a la lista de roles y se efectua una busqueda recursiva.
        /// En dicha busqueda recursiva se seguiran buscando Roles (familias y permisos) para ser agregados al arbol (lista de roles).
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<Rol> conseguirRolesDeUsuario(Usuario usuario)
        {
            List<Rol> listaRoles = new List<Rol>();
            string textoComando = "select distinct A.permisoID, P.descripcion, P.codigo from JoinUsuarioPermiso A join (select permisoID, descripcion, codigo from Permiso) P " +
                                  "ON(A.permisoID = P.permisoID) where A.permisoID IN(select permisoID from JoinUsuarioPermiso where usuarioID = @USER_ID)";

            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@USER_ID", usuario.Id));
            DataTable permisosDS = sqlHelper.ejecutarDataAdapter(textoComando, lista).Tables[0];
            foreach (DataRow i in permisosDS.Rows)
            {
                listaRoles.Add(buscarRecursivamente(Convert.ToInt32(i["permisoID"])));
            }

            return listaRoles;

        }

        /// <summary>
        /// Efectua una busqueda recursiva contra la base de datos por cada permiso recibido.
        /// Si es familia, se agrega a la lista de Roles y se llama a si mismo para continuar con la busqueda recursiva.
        /// Si es patente, solo se agrega a la lista de Roles
        /// </summary>
        /// <param name="permisoId"></param>
        /// <param name="familiaSuperior"></param>
        /// <returns></returns>
        private Rol buscarRecursivamente(int permisoId, Rol familiaSuperior = null)
        {
            string textoComando = "select A.IdPadrePermiso, A.IdHijoPermiso, P.codigo, P.descripcion, C.codigoPadre, C.descripcionPadre " +
                                  "from Permiso_Jerarquia A JOIN(select permisoID, codigo, descripcion from Permiso) P " +
                                  "ON(A.IdHijoPermiso = P.permisoID) " +
                                  "JOIN(select permisoID, codigo as codigoPadre, descripcion as descripcionPadre from Permiso) C " +
                                  "ON(A.IdPadrePermiso = C.permisoID) where A.IdPadrePermiso = @PERMISO_ID;";

            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@PERMISO_ID", permisoId));
            DataTable permisosHijosDT = sqlHelper.ejecutarDataAdapter(textoComando, lista).Tables[0];


            if (permisosHijosDT.Rows.Count > 0)
            {
                //Significa que tiene hijos
                Console.WriteLine("Encontre una familia con hijos!");
                Familia familia = new Familia();
                familia.Id = Convert.ToInt32(permisosHijosDT.Rows[0]["IdPadrePermiso"]);
                familia.Codigo = Convert.ToString(permisosHijosDT.Rows[0]["codigoPadre"]);
                familia.Descripcion = Convert.ToString(permisosHijosDT.Rows[0]["descripcionPadre"]);


                //Si tengo una familia sobre la familia actual. Si no, es la primera familia.
                if (familiaSuperior != null)
                {
                    familiaSuperior.agregar(familia);
                }

                //Entonces busco recursivamente ahora

                foreach (DataRow i in permisosHijosDT.Rows)
                {
                    Console.WriteLine("Busco recursivamente para la familia " + Convert.ToString(permisosHijosDT.Rows[0]["codigoPadre"]));
                    buscarRecursivamente(Convert.ToInt32(i["IdHijoPermiso"]), familia);
                }
                return familia;
            }
            else
            {
                //Significa que es un permiso sin hijo, una hoja del arbol.
                Console.WriteLine("Encontre una hoja!");
                string textoComandoParaHijo = "select permisoID, codigo, descripcion from Permiso where permisoID = @PERMISO_ID";
                List<SqlParameter> listaParametroHijo = new List<SqlParameter>();
                listaParametroHijo.Add(new SqlParameter("@PERMISO_ID", permisoId));
                DataRow permisoDR = sqlHelper.ejecutarDataAdapter(textoComandoParaHijo, listaParametroHijo).Tables[0].Rows[0];


                Permiso permiso = new Permiso();
                permiso.Codigo = Convert.ToString(permisoDR["codigo"]);
                permiso.Descripcion = Convert.ToString(permisoDR["descripcion"]);
                permiso.Id = Convert.ToInt32(permisoDR["permisoID"]);

                //Si tengo una familiaSuperior, agrego el permiso a ella
                if (familiaSuperior != null)
                {
                    familiaSuperior.agregar(permiso);
                }

                Console.WriteLine("Hoja: " + Convert.ToString(permisoDR["codigo"]));
                return permiso;
            }
        }
    }
}
