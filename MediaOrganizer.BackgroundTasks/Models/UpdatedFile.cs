using Windows.Storage;

namespace MediaOrganizer.BackgroundTasks.Models
{
    public sealed class UpdatedFile
    {
        public StorageFile File { get; set; }

        public string Filename { get; set; }

        public UpdatedFile(StorageFile file, string filename)
        {
            File = file;
            Filename = filename;
        }
    }
}
