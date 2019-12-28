using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaOrganizer.Core.Interfaces
{
    public interface IPickerService
    {
        Task<string> SelectFolderAsync();
    }
}
