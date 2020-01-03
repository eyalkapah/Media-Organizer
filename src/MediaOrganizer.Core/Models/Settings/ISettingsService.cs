using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganizer.Core.Models.Settings
{
    public interface ISettingsService
    {
        SettingsModel Instance { get; }

        Task SaveAsync();
    }
}
