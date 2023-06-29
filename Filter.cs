using System.Text;
using System.Xml;

namespace WfpXmlHelper
{
    public class Filter
    {
        public string Id;
        public string Name;
        public string Description;
        public List<string> Flags = new();
        public string LayerKey;
        public string ProviderKey;
        public string Weight;
        public string EffectiveWeight;
        public string Action;
        public string CalloutKey;
        public List<string> Condition = new();

        public Filter(XmlNode xNode)
        {
            Id = xNode.TextValue("filterId");
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
            LayerKey = xNode.TextValue("layerKey");
            ProviderKey = xNode.TextValue("providerKey");
            Weight = xNode.TextValue("weight/uint8");
            EffectiveWeight = xNode.TextValue("effectiveWeight/uint64");
            Action = xNode.TextValue("action/type");
            CalloutKey = xNode.TextValue("action/filterType");
            var conditionNodes = xNode.SelectNodes("filterCondition/item");
            if (conditionNodes != null)
            {
                foreach (XmlNode conditionNode in conditionNodes)
                {
                    Condition.Add(ProcessCondition(conditionNode));
                }
            }
        }

        private static string ProcessCondition(XmlNode xNode)
        {
            var key = xNode.TextValue("fieldKey")[15..];
            var match = Statics.MatchTypeDic[xNode.TextValue("matchType")];
            var value = ProcessConditionValue(xNode.SelectSingleNode("conditionValue")!);

            return $"{key} {match} {value}";
        }

        private static string ProcessConditionValue(XmlNode? xNode)
        {
            if (xNode == null) return string.Empty;
            var type = xNode.TextValue("type");
            return type switch
            {
                "FWP_UINT8" => xNode.TextValue("uint8"),
                "FWP_UINT16" => xNode.TextValue("uint16"),
                "FWP_UINT32" => xNode.TextValue("uint32"),
                "FWP_UINT64" => xNode.TextValue("uint64"),
                "FWP_INT8" => xNode.TextValue("int8"),
                "FWP_INT16" => xNode.TextValue("int16"),
                "FWP_INT32" => xNode.TextValue("int32"),
                "FWP_INT64" => xNode.TextValue("int64"),
                "FWP_FLOAT" => xNode.TextValue("float32"),
                "FWP_DOUBLE" => xNode.TextValue("double64"),
                "FWP_BYTE_ARRAY16_TYPE" => xNode.TextValue("byteArray16"),
                "FWP_BYTE_BLOB_TYPE" => Encoding.Unicode.GetString(Convert.FromHexString(xNode.TextValue("byteBlob/data"))) + " (" + xNode.TextValue("byteBlob/data") + ")",
                "FWP_SID" => xNode.TextValue("sid"),
                "FWP_SECURITY_DESCRIPTOR_TYPE" => xNode.TextValue("sd"),
                "FWP_TOKEN_INFORMATION_TYPE" => xNode.TextValue("tokenInformation"),
                "FWP_TOKEN_ACCESS_INFORMATION_TYPE" => xNode.TextValue("tokenAccessInformation"),
                "FWP_UNICODE_STRING_TYPE" => xNode.TextValue("unicodeString"),
                "FWP_BYTE_ARRAY6_TYPE" => xNode.TextValue("byteArray6"),
                "FWP_V4_ADDR_MASK" => xNode.TextValue("v4AddrMask"),
                "FWP_V6_ADDR_MASK" => xNode.TextValue("v6AddrMask"),
                "FWP_RANGE_TYPE" => ProcessConditionValue(xNode.SelectSingleNode("rangeValue/valueLow")) + " - " + ProcessConditionValue(xNode.SelectSingleNode("rangeValue/valueHigh")),
                _ => string.Empty,
            };
        }
    }
}
