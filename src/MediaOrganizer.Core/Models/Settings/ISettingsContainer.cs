using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganizer.Core.Models.Settings
{
    public interface ISettingsContainer
    {
        void CreateRoot(string containerName);

        ISettingsContainer GetOrCreateContainer(string containerName);

        T GetValueOrDefault<T>(string valueName, T defaultValue);

        void SetValue<T>(string containerName, T value);
    }
}
