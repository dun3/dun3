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
    public partial class Timer : System.Web.UI.Page
    {
        static Guid incidentId = new Guid("{97F6056E-1A7E-4f0e-A0ED-895A556BBDC5}");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Container GetLogItemz(string lastLogId)
        {
            System.Threading.Thread.Sleep(500);

            LogService.LogServiceContractClient client = new LogViewerTest.LogService.LogServiceContractClient();

            var itemz = client.FindSummaryLogItemzByIncidentId(incidentId);
            itemz.Sort((i, j) => j.Id.CompareTo(i.Id));

            Container c = new Container();

            c.LastId = itemz.Max(a => a.Id);

            int lastId = 0;
            if (int.TryParse(lastLogId, out lastId))
            {
                itemz = (from s in itemz
                         where s.Id > lastId
                         select s).ToList();
            }
            c.Text = string.Empty;
            foreach (var item in itemz)
            {
                c.Text += item.Id + ": " + item.Summary + " - ";
            }

            return c;
        }

        [Serializable]
        public class Container
        {
            public string Text;
            public long LastId;
        }
    }
}
