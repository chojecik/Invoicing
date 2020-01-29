﻿using System.ComponentModel.DataAnnotations;

namespace Invoicing.Core.Database.Entities
{
    public class Attachment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string DirectoryPath { get; set; }

        [Required]
        [MaxLength(4)]
        public string FileExtension { get; set; }
    }
}