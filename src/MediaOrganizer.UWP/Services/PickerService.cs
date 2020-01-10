using System;
using System.Threading.Tasks;
using MediaOrganizer.Core.Interfaces;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace MediaOrganizer.UWP.Services
{
    public class PickerService : IPickerService
    {
        public async Task<string> SelectFolderAsync()
        {
            var folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            var folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(folder);
                return folder.Path;
            }
            else
            {
                return null;
            }
        }
    }
}
