using EPiServer.Data.Dynamic;
using EPiServer.ServiceLocation;
using System.Collections;
using System.Linq;
using Zanganeh.RobotsTxtHandler.ViewModel;
using System;
using EPiServer.Data;

namespace Zanganeh.RobotsTxtHandler.Services
{
    public interface IRobotsTxtRepository
    {
        IOrderedQueryable<RobotsTxtItem> All();
        void Save(RobotsTxtItem robotsTxtItem);
        RobotsTxtItem FindBySiteId(string siteId);
        RobotsTxtItem Find(Guid id);
        void Delete(Guid id);
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

        public RobotsTxtItem Find(Guid id)
        {

            return Store.Load<RobotsTxtItem>(id);
        }

        public RobotsTxtItem FindBySiteId(string siteId)
        {
            return Store.Find<RobotsTxtItem>("SiteId", siteId).FirstOrDefault();
        }

        public void Delete(Guid id)
        {
            Store.Delete(id);
        }
    }
}
