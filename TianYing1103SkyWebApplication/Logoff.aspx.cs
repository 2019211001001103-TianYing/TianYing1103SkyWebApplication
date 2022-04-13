using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TianYing1103SkyWebApplication
{
    public partial class Logoff : System.Web.UI.Page
    {
        private void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
        }
    }
}