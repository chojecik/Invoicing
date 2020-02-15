using System.ComponentModel.DataAnnotations;

namespace Invoicing.Core.Database.Entities
{
    public class Attachment
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(200)]
        public string DirectoryPath { get; set; }

        [MaxLength(4)]
        public string FileExtension { get; set; }
    }
}
