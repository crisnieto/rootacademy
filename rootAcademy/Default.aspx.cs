using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BE;

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
                LabelBienvenido.Text = "Bienvenido " + User.Identity.Name;
                LabelBienvenidoMobile.Text = "Bienvenido " + User.Identity.Name;
                LoginLabelMobile.Visible = false;
                LogoutLabelMobile.Visible = true;
            }
            else
            {
                UsuarioLogueado.Visible = false;
                Logueate.Visible = true;
                LogoutLabel.Visible = false;
                LabelBienvenido.Visible = false;
                LabelBienvenidoMobile.Visible = false;
                LoginLabelMobile.Visible = true;
                LogoutLabelMobile.Visible = false;
            }
        }
    }
}