using System.Xml;

namespace WfpXmlHelper
{
    internal static class XmlExtension
    {
        internal static string TextValue(this XmlNode xNode, string path)
        {
            return xNode.SelectSingleNode(path)?.InnerText ?? string.Empty;
        }
    }
}
