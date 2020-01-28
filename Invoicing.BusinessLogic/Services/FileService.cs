using Invoicing.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;

namespace Invoicing.BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private const string tempDirectory = "C:\\TempUpload\\";
        public void DeleteFile()
        {
            throw new NotImplementedException();
        }

        public string ExportFile(IFormFile file)
        {
            if (!Directory.Exists(tempDirectory))
            {
                Directory.CreateDirectory(tempDirectory);
            }
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(tempDirectory, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return fullPath;
            }
            return null;
        }

        public string GetFileExtension(string path)
        {
            return path.Split('.').Last();
        }

        public void ImportFile()
        {
            throw new NotImplementedException();
        }
    }
}
