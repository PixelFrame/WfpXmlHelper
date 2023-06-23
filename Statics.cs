namespace WfpXmlHelper
{
    public static class Statics
    {
        public static Dictionary<string, string> ProfileDic = new()
        {
            { "0", "Any" },
            { "1", "Public" },
            { "2", "Private" },
            { "3", "Domain" },
        };

        public static Dictionary<string, string> MatchTypeDic = new()
        {
            { "FWP_MATCH_EQUAL" , "==" },
            { "FWP_MATCH_GREATER" , ">" },
            { "FWP_MATCH_LESS" , "<" },
            { "FWP_MATCH_GREATER_OR_EQUAL" , ">=" },
            { "FWP_MATCH_LESS_OR_EQUAL" , "<=" },
            { "FWP_MATCH_RANGE" , "In Range" },
            { "FWP_MATCH_FLAGS_ALL_SET" , "All Flags Set" },
            { "FWP_MATCH_FLAGS_ANY_SET" , "Any Flag Set" },
            { "FWP_MATCH_FLAGS_NONE_SET" , "No Flag Set" },
            { "FWP_MATCH_EQUAL_CASE_INSENSITIVE" , "== (Case Insensitive)" },
            { "FWP_MATCH_NOT_EQUAL" , "!=" },
            { "FWP_MATCH_PREFIX" , "Prefix" },
            { "FWP_MATCH_NOT_PREFIX" , "Not Prefix" },
        };

        public static Dictionary<string, string> DirectionDic = new()
        {
            { "MS_FWP_DIRECTION_IN", "Inbound" },
            { "MS_FWP_DIRECTION_OUT", "Outbound" },
        };
    }
}
