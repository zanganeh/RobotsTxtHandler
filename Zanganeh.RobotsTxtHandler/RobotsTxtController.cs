using System.Web.Mvc;

namespace Zanganeh.RobotsTxtHandler
{
	public class RobotsTxtController : Controller
	{
		public ActionResult Index()
		{
			return Content("User-agent: *");
		}
	}
}
