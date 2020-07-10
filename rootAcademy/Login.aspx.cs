using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;
using System.Web.Security;

namespace rootAcademy
{
    public partial class Login : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                new DVVH().verificarIntegridad();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
            }

            try
            {
                Usuario usuarioRecibido = new BLLUsuario().GetUsuario(username.Text, password.Text);

                Usuario usuarioLogin = new BLLUsuario().conseguirUsuarioLogIn(usuarioRecibido);

                FormsAuthentication.SetAuthCookie(usuarioLogin.Username, true);

                Response.Redirect("Default.aspx");
            }
            catch(Exception ex)
            {
                Label1.Text = "Error al ingresar los datos";
                Label1.Visible = true;
            }

        }
    }
}