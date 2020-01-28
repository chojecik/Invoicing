using Invoicing.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Invoicing.Core.Database.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Number { get; set; }

        [Required]
        [MaxLength(50)]
        public string Contractor { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal GrossAmount { get; set; }

        [Required]
        public decimal NetAmount { get; set; }

        [Required]
        public decimal VatAmount { get; set; }

        [Required]
        public InvoiceType InvoiceType { get; set; }

        public virtual Attachment Attachment { get; set; }

    }
}
