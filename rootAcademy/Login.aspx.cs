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
                bool errorDeIntegridad = false;
                string mensajeDeError = null;
                try
                {
                    new DVVH().verificarIntegridad();
                }
                catch (Exception ex)
                {
                    errorDeIntegridad = true;
                    mensajeDeError = "alert('" + ex.Message.Replace("\n", "\\n") + "');window.location ='Default.aspx';";
                }
                if (errorDeIntegridad && Sesion.Instancia().validar(Sesion.Instancia().conseguirUsuarioEnSesion(User.Identity), "AA099"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", mensajeDeError, true);
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuarioRecibido = new BLLUsuario().construirUsuarioRecibido(username.Text, password.Text);

                Usuario usuarioLogin = new BLLUsuario().conseguirUsuarioLogIn(usuarioRecibido);

                FormsAuthentication.SetAuthCookie(usuarioLogin.Username, true);

                Response.Redirect("Login.aspx");
            }
            catch(Exception ex)
            {
                Label1.Text = "Error al ingresar los datos";
                Label1.Visible = true;
            }

        }
    }
}