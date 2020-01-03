using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MediaOrganizer.Core.Models
{
    public class RegexPattern
    {
        [JsonProperty("guid")]
        public Guid Guid { get; set; }

        [JsonProperty("pattern")]
        public string Pattern { get; set; }

        [JsonProperty("replace_string")]
        public string ReplaceString { get; set; }

        [JsonProperty("word")]
        public string Word { get; set; }

        public RegexPattern(string word, string pattern, string replaceString = null)
        {
            Guid = Guid.NewGuid();
            Word = word;
            Pattern = pattern;
            ReplaceString = replaceString;
        }

        public RegexPattern()
        {
        }
    }
}
