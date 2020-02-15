using Invoicing.Core.Database.Entities;
using Invoicing.Core.Enums;
using System;
using System.Collections.Generic;
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
        public DateTime DateOfIssue { get; set; }


        [Required]
        public DateTime DateOfService { get; set; }

        [Required]
        public InvoiceType Type { get; set; }

        public bool IsPaid { get; set; }

        [Required]
        public int VatRate { get; set; }

        [Required]
        public decimal NetValue { get; set; }

        [Required]
        public decimal VatAmount { get; set; }

        [Required]
        public decimal GrossValue { get; set; }

        public InvoiceDetails[] Details { get; set; }

        [MaxLength(200)]
        public string FilePath { get; set; }

        [MaxLength(4)]
        public string FileExtension { get; set; }

    }
}
