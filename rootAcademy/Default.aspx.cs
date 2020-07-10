using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rootAcademy
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                UsuarioLogueado.Visible = true;
                Logueate.Visible = false;
                LogoutLabel.Visible = true;
                Label1.Text = "Bienvenido " + User.Identity.Name;
            }
            else
            {
                UsuarioLogueado.Visible = false;
                Logueate.Visible = true;
                LogoutLabel.Visible = false;
                Label1.Visible = false;
            }
        }
    }
}