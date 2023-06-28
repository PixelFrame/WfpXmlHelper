using System.Xml;

namespace WfpXmlHelper
{
    public class WfpState
    {
        public List<Filter> Filters = new();
        public List<Provider> Providers = new();

        public string CurrentItem { get; private set; } = "?";
        public int CurrentCount { get; private set; } = 0;
        public int CurrentMax { get; private set; } = 0;

        public void Load(string xml)
        {
            xml = xml.Remove(xml.LastIndexOf("</wfpstate>") + 11);
            xml = xml.Replace("&&", "&amp;&amp;");
            var xDoc = new XmlDocument();
            xDoc.LoadXml(xml);

            var filterNodes = xDoc.SelectNodes("/wfpstate/layers//filters/item");
            if (filterNodes != null)
            {
                CurrentItem = "Filters";
                CurrentCount = 0;
                CurrentMax = filterNodes.Count;
                foreach (XmlNode filterNode in filterNodes)
                {
                    CurrentCount++;
                    Filters.Add(new Filter(filterNode));
                }
            }

            var providerNodes = xDoc.SelectNodes("/wfpstate/providers/item");
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
