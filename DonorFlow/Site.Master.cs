using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonorFlow
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Page_Load logic for the master page
        }

        // Expose LinkButton15 via a public property with a different name
        public LinkButton LinkButton15Property
        {
            get { return this.LinkButton15; }
        }
        public LinkButton LinkButton1Property
        {
            get { return this.LinkButton1; }
        }
        public LinkButton LinkButton2Property
        {
            get { return this.LinkButton2; }
        }
    }
}

