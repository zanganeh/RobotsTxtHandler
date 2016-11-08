using EPiServer.Data.Dynamic;
using EPiServer.Data;

namespace Zanganeh.RobotsTxtHandler.ViewModel
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    public class RobotsTxtItem : IDynamicData
    {
        public Identity Id { get; set; }
        public string SiteId { get; set; }
        public string RobotxTxt { get; set; }
    }
}