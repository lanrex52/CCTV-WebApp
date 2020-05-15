using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCTV_App.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace CCTV_App.Pages.Player
{
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.None, NoStore = false)]
    public class DashboardModel : PageModel
    {
        public IList<string> FilesInContainer { get; set; }
        private string _storageConnectionString;
        private string _containerName;
        private readonly StorageFiles _storageFiles;

        public DashboardModel(IConfiguration config, StorageFiles storageFiles)
        {
            _storageConnectionString = config.GetConnectionString("storageConnectionString");
            _containerName = config["ContainerName"];
            _storageFiles = storageFiles;
        }
        public void OnGet()
        {
            FilesInContainer = _storageFiles.GetBlobFileListAsync(_storageConnectionString, _containerName).GetAwaiter().GetResult();
        }
    }
}