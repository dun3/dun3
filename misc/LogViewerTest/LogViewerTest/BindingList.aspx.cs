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
using LogViewerTest.Service.Log.DTO;
using System.Collections.Generic;

namespace LogViewerTest
{
    public partial class BindingList : System.Web.UI.Page
    {
        Guid i1 = new Guid("{97F6056E-1A7E-4f0e-A0ED-895A556BBDC5}");

        public List<SummaryLogItemDTO> SummaryLogItemz
        {
            get
            {
                using (LogService.LogServiceContractClient c = new LogViewerTest.LogService.LogServiceContractClient())
                {
                    return c.FindSummaryLogItemzByIncidentId(i1);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
