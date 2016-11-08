using System.Web.Mvc;

namespace Zanganeh.RobotsTxtHandler.Controllers
{
	public class RobotsTxtController : Controller
	{
		public ActionResult Index()
		{
			return Content("User-agent: *");
		}
	}
}
