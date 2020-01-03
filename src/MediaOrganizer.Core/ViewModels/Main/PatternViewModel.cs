using System;
using MediaOrganizer.Core.Models;

namespace MediaOrganizer.Core.ViewModels.Main
{
    public class PatternViewModel : BaseViewModel
    {
        private string _pattern;

        private string _repleaseString;

        private string _word;

        public Guid Guid { get; set; }

        public string Pattern
        {
            get => _pattern;
            set => SetProperty(ref _pattern, value);
        }

        public string ReplaceString
        {
            get => _repleaseString;
            set => SetProperty(ref _repleaseString, value);
        }

        public string Word
        {
            get => _word;
            set => SetProperty(ref _word, value);
        }

        public PatternViewModel(RegexPattern pattern)
        {
            Guid = pattern.Guid;
            Word = pattern.Word;
            Pattern = pattern.Pattern;
            ReplaceString = pattern.ReplaceString;
        }
    }
}
