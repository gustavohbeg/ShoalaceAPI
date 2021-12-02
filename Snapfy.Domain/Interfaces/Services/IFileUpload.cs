using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Services
{
    public interface IFileUpload
    {
        public string UploadBase64Image(string base64, string container, string format);
    }
}
