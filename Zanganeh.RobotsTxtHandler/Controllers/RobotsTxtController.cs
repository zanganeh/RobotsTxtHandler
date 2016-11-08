using EPiServer.Web;
using System.Web.Mvc;
using Zanganeh.RobotsTxtHandler.Services;

namespace Zanganeh.RobotsTxtHandler.Controllers
{
	public class RobotsTxtController : Controller
	{
        public RobotsTxtController(IRobotsTxtRepository robotsTxtRepository)
        {
            this.robotsTxtRepository = robotsTxtRepository;
        }

        public ActionResult Index()
        {
            var robotsTxt = robotsTxtRepository.FindBySiteId(SiteDefinition.Current.Id.ToString());
            if (robotsTxt != null)
            {
                return Content(robotsTxt.RobotxTxt);
            }

            return Content("User-agent: *");
        }

        readonly IRobotsTxtRepository robotsTxtRepository;
    }
}
