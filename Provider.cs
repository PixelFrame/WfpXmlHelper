using System.Xml;

namespace WfpXmlHelper
{
    public class Provider
    {
        public string Key;
        public string Name;
        public string Description;
        public List<string> Flags = new();
        public string ServiceName;

        public Provider(XmlNode xNode)
        {
            Key = xNode.TextValue("providerKey");
            Name = xNode.TextValue("displayData/name");
            Description = xNode.TextValue("displayData/description");
            var flagNodes = xNode.SelectNodes("flags/item");
            if (flagNodes != null)
            {
                foreach (XmlNode flagNode in flagNodes)
                {
                    Flags.Add(flagNode.InnerText);
                }
            }
            ServiceName = xNode.TextValue("serviceName");
        }
    }
}
