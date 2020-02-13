using Invoicing.Core.Enums;
using System;
using System.Collections.Generic;
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
        public DateTime DateOfIssue { get; set; }


        [Required]
        public DateTime DateOfService { get; set; }

        [Required]
        public InvoiceType Type { get; set; }

        public bool IsPaid { get; set; }

        [Required]
        public int VatRate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal NetValue { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal VatAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GrossValue { get; set; }

        public virtual Attachment Attachment { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<InvoiceDetails> Details { get; set; }
    }
}
