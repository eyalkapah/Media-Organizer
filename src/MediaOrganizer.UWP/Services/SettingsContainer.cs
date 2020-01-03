using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaOrganizer.Core.Models;
using MediaOrganizer.Core.Models.Settings;
using Windows.Storage;

namespace MediaOrganizer.UWP.Services
{
    public class SettingsContainer : ISettingsContainer
    {
        private ApplicationDataContainer _applicationDataContainer;

        public SettingsContainer(ApplicationDataContainer applicationDataContainer)
        {
            _applicationDataContainer = applicationDataContainer;
        }

        public void CreateRoot(string containerName)
        {
            _applicationDataContainer = ApplicationData.Current.LocalSettings.CreateContainer(containerName, ApplicationDataCreateDisposition.Always);
        }

        public ISettingsContainer GetOrCreateContainer(string containerName)
        {
            var container = _applicationDataContainer.CreateContainer(containerName, ApplicationDataCreateDisposition.Always);
            return new SettingsContainer(container);
        }

        public T GetValueOrDefault<T>(string valueName, T defaultValue)
        {
            if (!_applicationDataContainer.Values.TryGetValue(valueName, out var value))
            {
                return defaultValue;
            }

            return (T)value;
        }

        public void SetValue<T>(string key, T value)
        {
            _applicationDataContainer.Values[key] = value;
        }
    }
}
