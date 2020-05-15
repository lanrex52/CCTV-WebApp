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
    public class VideosModel : PageModel
    {
        public PaginatedList<string> FilesInContainer { get; set; }


        public string CurrentFilter { get; set; }
        private string _storageConnectionString;
        private string _containerName;


        private readonly StorageFiles _storageFiles;
        public VideosModel(IConfiguration config, StorageFiles storageFiles)
        {
            // Fetch the storage configuration (stored in appsettings.json locally, or App Settings on Cloud)
            _storageConnectionString = config.GetConnectionString("storageConnectionString");
            _containerName = config["ContainerName"];
            _storageFiles = storageFiles;
        }

        public async Task OnGet(string searchstring, int? pageIndex, string currentFilter)
        {

            // Populate the files list each time a page is called.
            try
            {
                if (searchstring != null)
                {
                    pageIndex = 1;
                }
                else
                {
                    searchstring = currentFilter;
                }

                CurrentFilter = searchstring;

                IEnumerable<String> file
                = _storageFiles.GetBlobFileListAsync(_storageConnectionString, _containerName).GetAwaiter().GetResult();


                file = file.Where(s => s.Contains(searchstring));

                int pagesize = 30;
                FilesInContainer = await PaginatedList<String>.CreateAsync(file, pageIndex ?? 1, pagesize);

            }
            catch 
            {

                throw;
            }

        }
        public async Task<PageResult> OnPostSearchAsync(string search, int? pageIndex, string currentFilter)
        {
            try
            {
                if (!String.IsNullOrEmpty(search))
                {
                    if (search != null)
                    {
                        pageIndex = 1;
                    }
                    else
                    {
                        search = currentFilter;
                    }

                    CurrentFilter = search;
                    //FilesInContainer = _storageFiles.GetBlobFileListAsync(_storageConnectionString, _containerName).GetAwaiter().GetResult();


                    IEnumerable<String> file = _storageFiles.GetBlobFileListAsync(_storageConnectionString, _containerName).GetAwaiter().GetResult();
                    file = await Task.Run(() => file.Where(s => s.Contains(search)));
                    int pagesize = 30;
                    FilesInContainer = await PaginatedList<String>.CreateAsync(file, pageIndex ?? 1, pagesize);

                }
            }
            catch 
            {

                throw;
            }
            return Page();
        }

    }
}