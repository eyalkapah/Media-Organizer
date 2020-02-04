using System.Linq;
using Windows.Storage;

namespace MediaOrganizer.UWP
{
    public static class Extensions
    {
        public static void AddOrUpdate(this ApplicationDataContainer applicationDataContainer, string key, string value)
        {
            if (applicationDataContainer.Values.Any())
            {
                if (applicationDataContainer.Values[key] != null)
                {
                    applicationDataContainer.Values[key] = value;
                    return;
                }
            }

            applicationDataContainer.Values.Add(key, value);
        }
    }
}
