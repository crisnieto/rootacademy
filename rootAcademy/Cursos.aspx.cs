using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace rootAcademy
{
    public partial class Cursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CursosWebService cursosWebService = new CursosWebService();
            XmlDocument cursos = cursosWebService.conseguirCursosActuales();
            XmlReader xmlReader = new XmlNodeReader(cursos);
            DataSet dataset = new DataSet();
            dataset.ReadXml(xmlReader);
            GridView1.DataSource = dataset;
            GridView1.DataBind();
        }

        protected void buscarCurso(string nombre)
        {
            CursosWebService cursosWebService = new CursosWebService();
            XmlDocument cursos = cursosWebService.buscarCurso(nombre);
            XmlReader xmlReader = new XmlNodeReader(cursos);
            DataSet dataset = new DataSet();
            dataset.ReadXml(xmlReader);
            GridView1.DataSource = dataset;
            try {
            GridView1.DataBind();
            } catch(Exception ex)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

            }
        }


        protected void Buscar_Click(object sender, EventArgs e)
        {
            buscarCurso(TextBox1.Text);  
        }
    }
}