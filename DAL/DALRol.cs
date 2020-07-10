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
        /// Se encarga de interactuar con la base de datos para obtener todos los roles del sistema.
        /// Obtiene por una parte todas las familias del primer nivel y efectua la busqueda recursiva para obtener
        /// todos los hijos y armar el arbol.
        /// Por ultimo obtiene todos los permisos huerfanos.
        /// </summary>
        /// <returns></returns>
        public List<Rol> conseguirRoles()
        {
            List<Rol> listaRoles = new List<Rol>();
            try
            {
                foreach (Familia familia in conseguirFamilias())
                {
                    listaRoles.Add(buscarRecursivamente(familia.Id));
                }
            }
            catch (Exception ex)
            {

            }
            listaRoles.AddRange(conseguirPermisosSinPadre());
            return listaRoles;

        }

        /// <summary>
        /// Se encarga de consultar a la base de datos los permisos huerfanos, es decir, aquellos que no tiene una Familia.
        /// </summary>
        /// <returns></returns>
        public List<Rol> conseguirPermisosSinPadre()
        {
            List<Rol> permisos = new List<Rol>();

            string textoComando = "SELECT t1.permisoID, t1.codigo, t1.descripcion, t2.IdHijoPermiso, t2.IdPadrePermiso " +
                                  "FROM permiso t1  left JOIN Permiso_Jerarquia t2 ON t2.IdPadrePermiso = t1.permisoID " +
                                  "or t2.IdHijoPermiso = t1.permisoID WHERE t2.IdHijoPermiso IS NULL and t2.IdPadrePermiso is null";
            DataTable dt = sqlHelper.ejecutarDataAdapter(textoComando).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                Permiso permiso = new Permiso();
                permiso.Codigo = (string)dr["codigo"];
                permiso.Descripcion = (string)dr["descripcion"];
                permiso.Id = (int)dr["permisoId"];
                permisos.Add(permiso);
            }

            return permisos;
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

        /// <summary>
        /// Se encarga de conseguir las familias del primer nivel.
        /// </summary>
        /// <returns></returns>
        public List<Familia> conseguirFamilias()
        {
            string textoComando = "select distinct A.IdPadrePermiso, B.codigo, B.descripcion from Permiso_Jerarquia A JOIN Permiso B ON (A.IdPadrePermiso = B.permisoID) WHERE A.IdPadrePermiso NOT IN (select IdHijoPermiso from Permiso_Jerarquia)";
            DataTable dt = sqlHelper.ejecutarDataAdapter(textoComando).Tables[0];

            List<Familia> listaFamilia = new List<Familia>();

            foreach (DataRow dr in dt.Rows)
            {
                Familia familia = new Familia();
                familia.Codigo = (string)dr["codigo"];
                familia.Id = (int)dr["idPadrePermiso"];
                familia.Descripcion = (string)dr["descripcion"];
                listaFamilia.Add(familia);
            }

            return listaFamilia;
        }

        /// <summary>
        /// Se encarga de devolver el resultado de ejecutar una query para validar si ya existe un codigo de rol.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public bool validarCodigoDeRol(string codigo)
        {
            //Verifico que no existan duplicados de Código
            string textoComando = "select codigo from Permiso where codigo = @CODIGO";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@CODIGO", codigo));
            return sqlHelper.ejecutarDataAdapter(textoComando, lista).Tables[0].Rows.Count == 0;
        }

        /// <summary>
        /// Se encarga de eliminarRecursivamente contra la base de datos un Rol.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int eliminarRecursivamente(int id)
        {

            string textoComandoHijos = "select idhijopermiso from permiso_jerarquia where IdPadrePermiso = @IDPadre";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@IDPadre", id));

            DataTable hijos = sqlHelper.ejecutarDataAdapter(textoComandoHijos, lista).Tables[0];

            foreach (DataRow dr in hijos.Rows)
            {
                eliminarRecursivamente((int)dr["idHijoPermiso"]);
            }

            string textoEliminarAsociación = "delete from permiso_jerarquia where IdHijoPermiso = @ID";
            List<SqlParameter> listaEliminarAsociacion = new List<SqlParameter>();
            listaEliminarAsociacion.Add(new SqlParameter("@ID", id));
            sqlHelper.ejecutarNonQuery(textoEliminarAsociación, listaEliminarAsociacion);

            string textoEliminarPadre = "delete from permiso where permisoId = @ID";
            List<SqlParameter> listaEliminar = new List<SqlParameter>();
            listaEliminar.Add(new SqlParameter("@ID", id));
            return sqlHelper.ejecutarNonQuery(textoEliminarPadre, listaEliminar);
        }


        /// <summary>
        /// Conseguir rol interactua con la base de datos y devuelve un Rol buscado en base al codigo.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public Rol conseguirRol(string codigo)
        {
            string textoComando = "select PermisoId, Codigo, Descripcion from Permiso where codigo = @CODIGO";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@CODIGO", codigo));
            DataTable dt = sqlHelper.ejecutarDataAdapter(textoComando, lista).Tables[0];

            Permiso permiso = new Permiso();

            permiso.Codigo = (string)dt.Rows[0]["codigo"];
            permiso.Descripcion = (string)dt.Rows[0]["descripcion"];
            permiso.Id = (int)dt.Rows[0]["permisoId"];

            return permiso;
        }

        /// <summary>
        /// crearRol se encarga de interactuar con la base de datos y guardar el Rol recibido en la base de datos.
        /// </summary>
        /// <param name="rol"></param>
        /// <param name="padre"></param>
        /// <returns></returns>
        public bool crearRol(Rol rol, Rol padre = null)
        {
            string textoComando = "insert into PERMISO (codigo, descripcion) VALUES (@CODIGO, @DESCRIPCION)";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@CODIGO", rol.Codigo));
            lista.Add(new SqlParameter("@DESCRIPCION", rol.Descripcion));
            bool resultado = Convert.ToBoolean(sqlHelper.ejecutarNonQuery(textoComando, lista));

            if (padre != null)
            {
                Rol rolCreado = conseguirRol(rol.Codigo);
                textoComando = "insert into permiso_jerarquia (IdPadrePermiso, IdHijoPermiso) VALUES (@IDPADRE, @IDHIJO)";
                lista = new List<SqlParameter>();
                lista.Add(new SqlParameter("@IDPADRE", padre.Id));
                lista.Add(new SqlParameter("@IDHIJO", rolCreado.Id));
                return Convert.ToBoolean(sqlHelper.ejecutarNonQuery(textoComando, lista));
            }

            return resultado;
        }

        /// <summary>
        /// asociarRolAUsuario se encarga de interactuar con la base de datos para guardar la asociacion entre un usuario y el rol recibido.
        /// </summary>
        /// <param name="rol"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool asociarRolAUsuario(Rol rol, Usuario usuario)
        {
            String textoComando = "insert into JoinUsuarioPermiso (usuarioID, permisoID) VALUES (@USUARIOID, @PERMISOID)";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@USUARIOID", usuario.Id));
            lista.Add(new SqlParameter("@PERMISOID", rol.Id));
            return Convert.ToBoolean(sqlHelper.ejecutarNonQuery(textoComando, lista));
        }


        /// <summary>
        /// desasociarDeUsuario se encarga de interactuar con la base de datos para eliminar la asociacion entre un usuario y el rol recibido.
        /// </summary>
        /// <param name="rol"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool desasociarDeUsuario(Rol rol, Usuario usuario)
        {
            String textoComando = "delete from JoinUsuarioPermiso where usuarioID = @USUARIOID and permisoId = @PERMISOID";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@USUARIOID", usuario.Id));
            lista.Add(new SqlParameter("@PERMISOID", rol.Id));
            return Convert.ToBoolean(sqlHelper.ejecutarNonQuery(textoComando, lista));
        }


        /// <summary>
        /// desasociarDeTodos se encarga de realizar la desasosiacion de un Rol para todos los usuarios que tengan el Rol.
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        public bool desasociarDeTodos(Rol rol)
        {
            String textoComando = "delete from JoinUsuarioPermiso where permisoId = @PERMISOID";
            List<SqlParameter> lista = new List<SqlParameter>();
            lista.Add(new SqlParameter("@PERMISOID", rol.Id));
            return Convert.ToBoolean(sqlHelper.ejecutarNonQuery(textoComando, lista));
        }

    }
}
