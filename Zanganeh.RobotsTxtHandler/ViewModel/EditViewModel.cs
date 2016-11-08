using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Zanganeh.RobotsTxtHandler.ViewModel
{
    public class EditViewModel
    {
        public string RobotText { get; set; }
        public string SelectedSiteId { get; set; }

        public IEnumerable<SelectListItem> Sites { get; internal set; }

    }
}
