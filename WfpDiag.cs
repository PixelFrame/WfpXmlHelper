using System.Xml;

namespace WfpXmlHelper
{
    public class WfpDiag
    {
        public List<NetEventClassifyDrop> NetEventsClassifyDrop = new();
        public List<Filter> InitialFilters = new();
        public List<Filter> AddedFilters = new();
        public List<Provider> Providers = new();

        public string CurrentItem { get; private set; } = "?";
        public int CurrentCount { get; private set; } = 0;
        public int CurrentMax { get; private set; } = 0;

        public void Load(string xml)
        {
            xml = xml.Replace("&&", "&amp;&amp;"); // Ampersands in filter conditions are not escaped in WFP XML output...
                                                   // Well, I believe no one expected them to be there in the first place.
            var xDoc = new XmlDocument();
            xDoc.LoadXml(xml);

            var netEventNodes = xDoc.SelectNodes("/wfpdiag/events/netEvent");
            if (netEventNodes != null)
            {
                CurrentItem = "Dropping Events";
                CurrentCount = 0;
                CurrentMax = netEventNodes.Count;
                foreach (XmlNode netEventNode in netEventNodes)
                {
                    CurrentCount++;
                    if (netEventNode.SelectSingleNode("type")!.InnerText.EndsWith("CLASSIFY_DROP"))
                        NetEventsClassifyDrop.Add(new NetEventClassifyDrop(netEventNode));
                }
            }

            var initFilterNodes = xDoc.SelectNodes("/wfpdiag/initialState//filters/item");
            if (initFilterNodes != null)
            {
                CurrentItem = "Initial Filters";
                CurrentCount = 0;
                CurrentMax = initFilterNodes.Count;
                foreach (XmlNode initFilterNode in initFilterNodes)
                {
                    CurrentCount++;
                    InitialFilters.Add(new Filter(initFilterNode));
                }
            }

            var addedFilterNodes = xDoc.SelectNodes("/wfpdiag/events//filterChange/filter");
            if (addedFilterNodes != null)
            {
                CurrentItem = "Added Filters";
                CurrentCount = 0;
                CurrentMax = addedFilterNodes.Count;
                foreach (XmlNode addedFilterNode in addedFilterNodes)
                {
                    CurrentCount++;
                    AddedFilters.Add(new Filter(addedFilterNode));
                }
            }

            var providerNodes = xDoc.SelectNodes("/wfpdiag/initialState/providers/item");
            if (providerNodes != null)
            {
                CurrentItem = "Providers";
                CurrentCount = 0;
                CurrentMax = providerNodes.Count;
                foreach (XmlNode providerNode in providerNodes)
                {
                    CurrentCount++;
                    Providers.Add(new Provider(providerNode));
                }
            }
        }
    }
}
