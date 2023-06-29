using System.Xml;

namespace WfpXmlHelper
{
    public class NetEvents
    {
        public List<NetEventClassifyDrop> NetEventsClassifyDrop = new();

        public string CurrentItem { get; private set; } = "?";
        public int CurrentCount { get; private set; } = 0;
        public int CurrentMax { get; private set; } = 0;

        public void Load(string xml)
        {
            var xDoc = new XmlDocument();
            xDoc.LoadXml(xml);

            if (NetEventsClassifyDrop.Count > 0)
                NetEventsClassifyDrop.Clear();

            var netEventNodes = xDoc.SelectNodes("/netEvents/item");
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
        }
    }
}
