using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;

namespace LogViewerTest
{
    public partial class JQueryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string[] GetDetails(string customerId)
        {
            System.Threading.Thread.Sleep(500);

            return new string[] { "lalalalallalalalal<br>lalalalal<br>lalallala<br>lalalalallalalalal<br>lalalalal<br>lalallala", "lalalalallalalalal<br>lalalalal<br>lalallala<br>lalalalallalalalal<br>lalalalal<br>lelelele" };
        }
    }
}
