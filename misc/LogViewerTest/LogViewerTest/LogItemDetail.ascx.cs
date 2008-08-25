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
using LogViewerTest.Service.Log;
using LogViewerTest.Service.Log.DTO;

namespace LogViewerTest
{
    public partial class LogItemDetail : System.Web.UI.UserControl
    {
        public string LogId
        {
            get
            {
                return (string)ViewState["ID"];
            }
            set { ViewState["ID"] = value; }
        }

        protected long GetId()
        {
            long result;
            if (long.TryParse(LogId, out result))
            {
                return result;
            }
            return 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            using (LogService.LogServiceContractClient c = new LogViewerTest.LogService.LogServiceContractClient())
            {
                LogItemDTO dto = c.FindLogItem(new LogViewerTest.Service.Log.DTO.LogIdDTO() { Id = GetId() });

                lblId.Text = dto.Id.ToString();
                lblMessage.Text = dto.Message;
                lblTitle.Text = dto.Title;
            }
        }
    }
}