using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganizer.Core.Models
{
    public class SimpleTextDialogParametersModel
    {
        public Action PrimaryButtonAction { get; set; }
        public string PrimaryButtonText { get; set; } = "OK";
        public Action SecondaryButtonAction { get; set; }
        public string SecondaryButtonText { get; set; } = "Cancel";
        public string Text { get; set; }
        public string Title { get; set; }
    }
}
