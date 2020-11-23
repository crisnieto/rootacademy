using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace rootAcademy
{
    public partial class BackupRestore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/backups"));
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }
                GridView1.DataSource = files;
                GridView1.DataBind();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Restore_Click(object sender, EventArgs e)
        {
            string filePath = (sender as Button).CommandArgument;
            new BLL.BLLBackupManager().ejecutarRestore(filePath);
            Response.Redirect("Logout.aspx");
        }


        protected void Backup_Click(object sender, EventArgs e)
        {
            new BLL.BLLBackupManager().crearBackup(Server.MapPath("~/backups") + "\\backup-" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH-mm-ssZ") + ".sql");
            Response.Redirect(Request.RawUrl);
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