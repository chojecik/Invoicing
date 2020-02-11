using Invoicing.Core.Database.Entities;
using Invoicing.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Invoicing.Web.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string InvoiceNumber { get; set; }

        [Required]
        public Contractor Contractor { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal GrossAmount { get; set; }

        [Required]
        public int VatRate { get; set; }

        [Required]
        public decimal NetAmount { get; set; }

        [Required]
        public decimal VatAmount { get; set; }

        [Required]
        public InvoiceType Type { get; set; }

        [Required]
        [MaxLength(200)]
        public string FilePath { get; set; }

        [MaxLength(4)]
        public string FileExtension { get; set; }
    }
}
