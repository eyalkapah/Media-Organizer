using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganizer.Core.Models
{
    public class RegexPattern
    {
        public Guid Guid { get; set; }

        public string Pattern { get; private set; }
        public string ReplaceString { get; set; }
        public string Word { get; set; }

        public RegexPattern(string word, string pattern, string replaceString = null)
        {
            Guid = Guid.NewGuid();
            Word = word;
            Pattern = pattern;
            ReplaceString = replaceString;
        }
    }
}
