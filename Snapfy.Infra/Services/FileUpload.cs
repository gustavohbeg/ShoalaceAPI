using Azure.Storage.Blobs;
using Shoalace.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Infra.Services
{
    public class FileUpload : IFileUpload
    {
        public string UploadBase64Image(string base64, string container)
        {
            string fileName = Guid.NewGuid().ToString() + ".png";
            
            //var data = new Regex(@"");

            byte[] imageBytes = Convert.FromBase64String(base64);

            var blobClient = new BlobClient("DefaultEndpointsProtocol=https;AccountName=shoalace;AccountKey=OvQkCTJERLT6bUjXEPl3yNWABpIkWOQEHnXgWMRWAusramyFMkHdO5KFilvLEeLq8w/hz36ikAUyWfA0ghY42g==;EndpointSuffix=core.windows.net", container, fileName);

            using (var stream = new MemoryStream(imageBytes))
            {
                blobClient.Upload(stream);
            }

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
