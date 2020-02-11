using Invoicing.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoicing.Core.Database.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Number { get; set; }

        [Required]
        public virtual Contractor Contractor { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GrossAmount { get; set; }

        [Required]
        public int VatRate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal NetAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal VatAmount { get; set; }

        [Required]
        public InvoiceType Type { get; set; }

        public virtual Attachment Attachment { get; set; }
        public virtual User User { get; set; }
    }
}
