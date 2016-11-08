using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System.Web.Mvc;
using System.Web.Routing;

namespace Zanganeh.RobotsTxtHandler
{
	[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
	public class RobotsTxtHandlerInitialization : IInitializableModule
	{
		public void Initialize(InitializationEngine context)
		{
			RouteTable.Routes.MapRoute("RobotsTxtHandlerRoute", "robots.txt", new { controller = "RobotsTxt", action = "Index" });
        }

        public void Uninitialize(InitializationEngine context)
		{
		}
	}
}
