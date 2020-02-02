using Invoicing.BusinessLogic.Interfaces;
using Invoicing.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Invoicing.BusinessLogic.Helpers;

namespace Invoicing.BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private const string tempDirectory = "C:\\TempUpload\\";
        private const string destinationDirectory = "D:\\Faktury\\";

        public void DeleteFile(string path)
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

        public void ImportFile(string path)
        {
            throw new NotImplementedException();
        }

        public void MoveFile(string sourceFile, InvoiceType type, DateTime date)
        {
            var fileName = sourceFile.Split('\\').Last();

            var destPath = GenerateDirectoryName(type, date);
            var destFile = Path.Combine(destPath, fileName);

           Directory.CreateDirectory(destPath);
           File.Move(sourceFile, destFile);
        }

        private string GenerateDirectoryName(InvoiceType type, DateTime date)
        {
            var sb = new StringBuilder();

            sb.Append(destinationDirectory);

            sb.Append(date.Year);
            sb.Append("\\");
            sb.Append(((Month)date.Month).GetDescription());
            sb.Append("\\");


            if (type == InvoiceType.Cost)
            {
                sb.Append("Faktury kosztowe");
            }
            else if(type == InvoiceType.Sale)
            {
                sb.Append("Faktury sprzedaży");
            }
            sb.Append("\\");

            return sb.ToString();
        }
    }
}
