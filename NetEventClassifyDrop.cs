using System.Text;
using System.Xml;

namespace WfpXmlHelper
{
    public class NetEventClassifyDrop
    {
        public DateTime TimeStamp;
        public List<string> Flags = new();
        public string IPVersion;
        public string IPProtocol;
        public string LocalAddress;
        public string RemoteAddress;
        public string LocalPort;
        public string RemotePort;
        public string AppId;
        public string UserId;
        public string PackageSid;
        public string FilterId;
        public string Direction;
        public string OriginalProfile;
        public string CurrentProfile;

        public NetEventClassifyDrop(XmlNode xNode)
        {
            var headerNode = xNode.SelectSingleNode("header")!;
            var classifyDropNode = xNode.SelectSingleNode("classifyDrop")!;

            TimeStamp = DateTime.Parse(headerNode.TextValue("timeStamp"));
            var flagNodes = headerNode.SelectNodes("flags/item");
            if (flagNodes != null)
            {
                foreach (XmlNode flagNode in flagNodes)
                {
                    Flags.Add(flagNode.InnerText);
                }
            }
            IPVersion = headerNode.TextValue("ipVersion");
            IPProtocol = headerNode.TextValue("ipProtocol");
            LocalAddress = headerNode.TextValue("localAddrV4") + headerNode.TextValue("localAddrV6");
            RemoteAddress = headerNode.TextValue("remoteAddrV4") + headerNode.TextValue("remoteAddrV6");
            LocalPort = headerNode.TextValue("localPort");
            RemotePort = headerNode.TextValue("remotePort");
            AppId = Encoding.Unicode.GetString(Convert.FromHexString(headerNode.TextValue("appId/data")));
            UserId = headerNode.TextValue("userId");
            PackageSid = headerNode.TextValue("packageSid");
            FilterId = classifyDropNode.TextValue("filterId");
            Direction = Statics.DirectionDic[classifyDropNode.TextValue("msFwpDirection")];
            OriginalProfile = Statics.ProfileDic[classifyDropNode.TextValue("originalProfile")];
            CurrentProfile = Statics.ProfileDic[classifyDropNode.TextValue("currentProfile")];
        }
    }
}
