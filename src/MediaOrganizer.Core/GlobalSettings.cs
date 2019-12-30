using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            new RegexPattern("נתי מדיה")
        };
    }
}
