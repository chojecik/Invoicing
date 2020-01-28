using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoicing.BusinessLogic.Interfaces
{
    /// <summary>
    /// Contains I/O methods for files management
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Saving file to local directory
        /// </summary>
        /// <param name="file">File to save</param>
        /// <returns>Returns directory path containing saved file</returns>
        string ExportFile(IFormFile file);

        void ImportFile();
        void DeleteFile();

        /// <summary>
        /// Gets file extension
        /// </summary>
        /// <param name="path">Full path of file</param>
        /// <returns>Returns file extension</returns>
        string GetFileExtension(string path);
    }
}
