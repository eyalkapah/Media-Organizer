using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganizer.Core.Enums
{
    public enum ScanStatus
    {
        Idle,
        UpToDate,
        Scanning,
        Moving,
        Failure
    }
}
