using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace rootAcademy
{
    /// <summary>
    /// Summary description for CursosWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CursosWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public XmlDocument conseguirCursosActuales()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Server.MapPath("~/Cursos.xml"));
            return xmlDoc;
        }

        [WebMethod]
        public XmlDocument buscarCurso(string nombre)
        {
            // Cargar el XML.

            CursosWebService cursosWebService = new CursosWebService();

            XmlDocument cursos = cursosWebService.conseguirCursosActuales();

            
            //Filtrar por Nombre (Busqueda con case-insensitive)

            XmlNodeList nodes = cursos.SelectNodes("/Cursos/Curso[Nombre[contains(translate(., 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), '" + nombre + "')]]");

            // Crear documento para filtrar y cargar nodos filtrados.

            XmlDocument documentoFiltrado = new XmlDocument();
            documentoFiltrado.Load(new StringReader("<Cursos/>"));


            foreach (XmlNode node in nodes)
            {
                XmlNode cursoCopiado = documentoFiltrado.ImportNode(node, true);
                documentoFiltrado.DocumentElement.AppendChild(cursoCopiado);
            }

            return documentoFiltrado;
        }
    }
}
