using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCTV_App.Helper
{
    
    public class StorageFiles
    {
        private readonly ILogger<StorageFiles> _logger;
        

        n
        public StorageFiles(ILogger<StorageFiles> logger)
        {
            _logger = logger;
        }
        public async Task<List<string>> GetBlobFileListAsync(string storageConnectionString, string containerName)
        {
            try
            {
                // Get Reference to Blob Container
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                // Fetch info about files in the container
                // Note: Loop with BlobContinuationToken to fetch results in pages. Pass null as currentToken to fetch all results.
                // BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(currentToken: null);
                // Extract the URI of the files into a new list
                List<IListBlobItem> fileUris = new List<IListBlobItem>();
                List<string> fileUri = new List<string>();
                BlobContinuationToken continuationToken = null;
                do
                {
                    BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(
                        prefix: null,
            useFlatBlobListing: true,
            blobListingDetails: BlobListingDetails.None,
            maxResults: 10000,
           continuationToken,
            options: null,
            operationContext: null
                        );

                    continuationToken = resultSegment.ContinuationToken;
                    // IEnumerable<IListBlobItem> blobItems = resultSegment.Results;
                    // IEnumerable<IListBlobItem> blobItems = resultSegment.Results;

                    fileUris.AddRange(resultSegment.Results);

                    //foreach (var blobItem in blobItems)
                    //{
                    //    //fileUris.Add(blobItem.StorageUri.PrimaryUri.ToString());
                    //    fileUris.Add(blobItem.StorageUri.PrimaryUri.ToString());

                    //}

                } while (continuationToken != null);
                //var file = from s in fileUris
                //           where s.Contains("Account Open Office", StringComparison.InvariantCultureIgnoreCase)
                //           select s;


                foreach (var item in fileUris)
                {
                    fileUri.Add(item.StorageUri.PrimaryUri.ToString());
                }
                //var file = from s in fileUri
                //           where s.Contains("Account Open Office", StringComparison.InvariantCultureIgnoreCase)
                //           select s;

                //var file = from s in fileUri
                //           where s.Contains(searchstring, StringComparison.InvariantCultureIgnoreCase)
                //           select s;
                //if (!String.IsNullOrEmpty(searchterm))
                //{
                //    file = file.Where(s => s.Contains(searchterm, StringComparison.InvariantCultureIgnoreCase));
                //}

                // return  file.ToList();
                return fileUri.ToList();

                

                // return fileUris;

                //CloudBlobDirectory dir =  container.GetBlockBlobReference("");

            }
            catch (System.Exception e)
            {
                // Note: When using ASP.NET Core Web Apps, to output to streaming logs, use ILogger rather than System.Diagnostics
                _logger.LogError($"Exception occurred while attempting to list files on server: {e.Message}");
                return null;         // or throw e; if you want to bubble the exception up to the caller
            }
        
        
    }
    }
}
