using System.Collections.Generic;
using MediaOrganizer.Core.Models;
using MediaOrganizer.Core.Models.Settings;

namespace MediaOrganizer.Core
{
    public class GlobalSettings
    {
        public FolderSettings FolderSettings = new FolderSettings
        {
            DestinationFolder = "",

            FileAction = "Move",

            SourceFolder = "D:\\Downloads\\Telegram Desktop",

            Patterns = new List<RegexPattern>
            {
                new RegexPattern("Nati Media", "נתי מדיה")
            }
        };
    }
}
