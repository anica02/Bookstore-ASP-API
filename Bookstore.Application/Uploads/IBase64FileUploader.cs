using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Uploads
{
    public interface IBase64FileUploader
    {
        public enum UploadType
        {
           BookImage
        }

        bool IsExtensionValid(string base64File);
        string GetExtension(string base64File);
        string Upload(string base64File, UploadType type);
        IEnumerable<string> Upload(IEnumerable<string> base64Files, UploadType type);
    }
}
