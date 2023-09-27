using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WfpXmlHelper
{
    public class Callout
    {
        public string CalloutKey;
        public string Id;
        public string Name;
        public string Description;
        public List<string> Flags = new();
        public string ProviderKey;
        public string ApplicableLayer;

        public Callout(XmlNode? xNode) 
        {
            if (xNode == null)
            {
                CalloutKey = "NULL";
                Id = "NULL";
                Name = "NULL";
                Description = "CalloutKey does not exist... Check if XML is valid";   
                ProviderKey = "NULL";
                ApplicableLayer = "NULL";
            }
            else
            {
                CalloutKey = xNode.TextValue("calloutKey");
                Id = xNode.TextValue("calloutId");
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
                ProviderKey = xNode.TextValue("providerKey");
                ApplicableLayer = xNode.TextValue("applicableLayer");
            }
        }
    }
}
