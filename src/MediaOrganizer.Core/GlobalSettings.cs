using System.Collections.Generic;
using MediaOrganizer.Core.Models;

namespace MediaOrganizer.Core
{
    public static class GlobalSettings
    {
        public const string DestinationFolder = "";

        public const string FileAction = "Move";
        public const string SourceFolder = "D:\\Downloads\\Telegram Desktop";

        public static List<RegexPattern> Patterns = new List<RegexPattern>
        {
            new RegexPattern("Nati Media", "נתי מדיה")
        };
    }
}
