using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agregador
{
    public partial class prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = new RestAPI("http://manage.camilyo.us/api/accounts/5777/suspend?suspendAllAssets=true", HttpVerb.PUT).MakeRequest();
        }
    }
}