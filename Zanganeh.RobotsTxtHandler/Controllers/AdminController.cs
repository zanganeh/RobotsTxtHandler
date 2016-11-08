using EPiServer.Framework.Web.Mvc;
using EPiServer.PlugIn;
using EPiServer.Security;
using EPiServer.Shell;
using EPiServer.Web;
using System;
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
        Url = Const.ModuleUrlBase + Const.Separator + Const.ModuleAdminController + Const.Separator + Const.ModuleAdminIndexAction,
        RequiredAccess = AccessLevel.Administer)]
    public class AdminController : Controller
    {
        public AdminController(ISiteDefinitionRepository siteDefinitionRepository, IRobotsTxtRepository robotsTxtRepository)
        {
            this.siteList = siteDefinitionRepository.List();
            this.robotsTxtRepository = robotsTxtRepository;

        }
        public ActionResult Index()
        {
            var robotTxts = robotsTxtRepository.All().ToList();

            var model = new AdminViewModel
            {
                Sites = GetSitesList(robotTxts.Select(a => a.SiteId)),
                AvailableRobotTxts = robotTxts.Select(a => new AvailableRobotTxt { Id = a.Id.ExternalId, Name = siteList.Single(s => s.Id.ToString() == a.SiteId).Name })
            };

            return PluginPartialView(Const.ModuleAdminIndexAction, model);
        }

        [HttpPost]
        public ActionResult Index(AdminViewModel model)
        {
            robotsTxtRepository.Save(new RobotsTxtItem { SiteId = model.SelectedSite, RobotxTxt = model.RobotText });

            return Redirect(Const.ModuleUrlBase + Const.Separator + Const.ModuleAdminController + Const.Separator + Const.ModuleAdminIndexAction);
        }

        public ActionResult Edit(Guid id)
        {
            var robotsTxt = robotsTxtRepository.Find(id);
            var selectedSiteIds = robotsTxtRepository.All().ToList().Select(a => a.SiteId).Except(new[] { robotsTxt.SiteId.ToString() });

            var model = new EditViewModel
            {
                Sites = GetSitesList(selectedSiteIds),
                RobotText = robotsTxt.RobotxTxt,
                SelectedSiteId = robotsTxt.SiteId
            };

            return PluginPartialView(Const.ModuleAdminEditAction, model);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, EditViewModel model)
        {
            var robotsTxt = robotsTxtRepository.Find(id);
            robotsTxt.SiteId = model.SelectedSiteId;
            robotsTxt.RobotxTxt = model.RobotText;
            robotsTxtRepository.Save(robotsTxt);

            return Redirect(Const.ModuleUrlBase + Const.Separator + Const.ModuleAdminController + Const.Separator + Const.ModuleAdminIndexAction);
        }

        public ActionResult Delete(Guid id)
        {
            robotsTxtRepository.Delete(id);

            return Redirect(Const.ModuleUrlBase + Const.Separator + Const.ModuleAdminController + Const.Separator + Const.ModuleAdminIndexAction);
        }

        ActionResult PluginPartialView(string action, object model)
        {
            var viewPath = Paths.ToResource(GetType(), $"Views/Admin/{action}.cshtml");

            return PartialView(viewPath, model);
        }
        IList<SelectListItem> GetSitesList(IEnumerable<string> currentSiteIDs)
        {
            return siteList
                .Where(a => currentSiteIDs.All(cs => new Guid(cs) != a.Id))
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
        }

        readonly IRobotsTxtRepository robotsTxtRepository;
        private readonly IEnumerable<SiteDefinition> siteList;
    }
}