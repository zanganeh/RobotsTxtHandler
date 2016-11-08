using EPiServer.PlugIn;
using EPiServer.Security;
using EPiServer.Shell;
using EPiServer.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Zanganeh.RobotsTxtHandler.Services;
using Zanganeh.RobotsTxtHandler.ViewModel;

namespace Zanganeh.RobotsTxtHandler.Controllers
{
    [GuiPlugIn(
        Area = EPiServer.PlugIn.PlugInArea.AdminMenu,
        DisplayName = "Robots.txt Handler",
        Description = "Robots.txt Handler",
        Url = "~/EPiServer/RobotsTxtHandler/Admin/Index",
        RequiredAccess = AccessLevel.Administer)]

    public class AdminController : Controller
    {
        public AdminController(ISiteDefinitionRepository siteDefinitionRepository, IRobotsTxtRepository robotsTxtRepository)
        {
            this.siteDefinitionRepository = siteDefinitionRepository;
            this.robotsTxtRepository = robotsTxtRepository;
        }
        public ActionResult Index()
        {

            var model = new AdminViewModel
            {
                Sites = GetSitesList(),
                RobotTxts = robotsTxtRepository.All().ToList()
            };

            return PluginPartialView(model);
        }


        [HttpPost]
        public ActionResult Index(AdminViewModel model)
        {
            model.Sites = GetSitesList();

            robotsTxtRepository.Save(new RobotsTxtItem { SiteId = model.SelectedSite, RobotxTxt = model.RobotText });

            return PluginPartialView(model);
        }

        ActionResult PluginPartialView(object model)
        {
            var viewPath = Paths.ToResource(GetType(), "Views/Admin/Index.cshtml");

            return PartialView(viewPath, model);
        }
        IList<SelectListItem> GetSitesList()
        {
            return siteDefinitionRepository.List().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
        }

        readonly ISiteDefinitionRepository siteDefinitionRepository;
        readonly IRobotsTxtRepository robotsTxtRepository;
    }
}