using Invoicing.Core.Enums;
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

        /// <summary>
        /// Loads file from specified directory
        /// </summary>
        /// <param name="path">Absolute path of the file</param>
        void ImportFile(string path);

        /// <summary>
        /// Deletes file from specified dirictory
        /// </summary>
        /// <param name="path">Absolute path of the file</param>
        void DeleteFile(string path);

        /// <summary>
        /// Gets file extension
        /// </summary>
        /// <param name="path">Full path of file</param>
        /// <returns>Returns file extension</returns>
        string GetFileExtension(string path);

        /// <summary>
        /// Moves file from temporary directory to destination directory based on invoice type and date
        /// </summary>
        /// <param name="sourcePath">Absolute path of the file in temporary directory</param>
        /// <param name="type">Type of the invoice (cost/sale)</param>
        /// <param name="date">Date of the invoice</param>
        void MoveFile(string sourcePath, InvoiceType type, DateTime date);
    }
}
