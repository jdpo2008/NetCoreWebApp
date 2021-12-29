using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NetCoreWebApp.Application.DTOs.User;
using NetCoreWebApp.Application.Interfaces;
using NetCoreWebApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApp.Product.Microservice.Services
{
    public class ImageService : IImageServices
    {
        public static IWebHostEnvironment _environment;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _storageConnectionString;

        public ImageService(IWebHostEnvironment environment, BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _environment = environment;
            _blobServiceClient = blobServiceClient;
            _storageConnectionString = configuration.GetConnectionString("AzureBlobStorage");
        }
        public async  Task<Response<String>> UploadImage(ImageRequest objfile, string id)
        {
            if (objfile.File.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + id + "_" + objfile.File.FileName))
                    {
                        objfile.File.CopyTo(filestream);
                        filestream.Flush();
                        return new Response<string>("\\uploads\\" + id + "_" + objfile.File.FileName);
                    }
                }
                catch (Exception ex)
                {
                    return new Response<string>(ex.ToString());
                }
            }
            else
            {
                return new Response<string>("Unsuccessful");
            }
        }

        //public async Task<Response<String>> UploadBlodStorage(ImageRequest model, string id)
        //{
        //    try
        //    {
        //        if(model.File != null) 
        //        {
        //            var blobContainer = _blobServiceClient.GetBlobContainerClient("upload-file");

        //            var blobClient = blobContainer.GetBlobClient(model.File.FileName);

        //            await blobClient.UploadAsync(model.File.OpenReadStream());

        //            return new Response<string>(blobClient.Uri.ToString());
        //        }  
        //        else
        //        {
        //            return new Response<string>("Unsuccessful");
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response<string>(ex.ToString());
        //    }
            
        //}

        public async Task<Response<string>> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            try
            {
                var container = new BlobContainerClient(_storageConnectionString, "file-container");
                var createResponse = await container.CreateIfNotExistsAsync();
                if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                    await container.SetAccessPolicyAsync(PublicAccessType.Blob);
                var blob = container.GetBlobClient(fileName);
                await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
                return new Response<string>(blob.Uri.ToString());
            }
            catch (Exception ex)
            {

                return new Response<string>(ex.ToString());
            }
          
        }

        public async Task<Response<string>> DownloadFile(string fileName)
        {
            AccountSasBuilder sasBuilder = new AccountSasBuilder()
            {
                Services = AccountSasServices.Blobs | AccountSasServices.Files,
                ResourceTypes = AccountSasResourceTypes.All,
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(1),
                Protocol = SasProtocol.Https
            };

            sasBuilder.SetPermissions(AccountSasPermissions.Read); // | AccountSasPermissions.Write

            StorageSharedKeyCredential key = new StorageSharedKeyCredential("MiNombreDeCuenta", "MiClaveDeCuenta");

            // Generar a partir de la clave de acceso, el token SAS:
            string sasToken = sasBuilder.ToSasQueryParameters(key).ToString();

            // Generar la dirección URL completa incluyendo el token SAS:
            UriBuilder fullUri = new UriBuilder()
            {
                Scheme = "https",
                //Host = string.Format("{0}.blob.core.windows.net", CloudConfigurationManager.GetSetting("accountName")),
                //Path = string.Format("{0}/{1}", nombreContainer, nombreArchivo),
                Query = sasToken
            };

            return new Response<string>(fullUri.Uri.AbsolutePath.ToString());
        }
    }
}
