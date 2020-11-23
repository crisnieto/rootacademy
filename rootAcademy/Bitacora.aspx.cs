using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;

namespace rootAcademy
{
    public partial class Bitacora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadBitacora();
        }

        private void loadBitacora()
        {
            BLLBitacora bLLBitacora = new BLLBitacora();
            try
            {
                List<BE.Bitacora> bitacoras = bLLBitacora.conseguirBitacorasSinUsuario(User.Identity, new DateTime(2008, 01, 01), DateTime.Today.AddDays(1));
                GridView1.DataSource = bitacoras;
                GridView1.DataBind();
            }
            catch (ForbiddenException ex)
            {
                Response.Redirect("Forbidden.aspx");
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

    }
}