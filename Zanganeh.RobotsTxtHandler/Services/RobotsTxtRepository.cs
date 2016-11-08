using EPiServer.Data.Dynamic;
using EPiServer.ServiceLocation;
using System.Collections;
using System.Linq;
using Zanganeh.RobotsTxtHandler.ViewModel;
using System;

namespace Zanganeh.RobotsTxtHandler.Services
{
    public interface IRobotsTxtRepository
    {
        IOrderedQueryable<RobotsTxtItem> All();
        void Save(RobotsTxtItem robotsTxtItem);
    }

    [ServiceConfiguration(typeof(IRobotsTxtRepository))]
    public class RobotsTxtRepository : IRobotsTxtRepository
    {
        static DynamicDataStore Store
        {
            get { return typeof(RobotsTxtItem).GetStore(); }
        }

        public void Save(RobotsTxtItem robotsTxtItem)
        {
            Store.Save(robotsTxtItem);
        }

        public IOrderedQueryable<RobotsTxtItem> All()
        {
            return Store.Items<RobotsTxtItem>();
        }
    }
}
