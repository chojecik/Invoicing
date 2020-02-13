using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoicing.Core.Database.Entities
{
    public class InvoiceDetails
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; }

        [MaxLength(100)]
        public string PKWiU { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal NetPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Unit { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal NetValue { get; set; }

        [Required]
        public int VatRate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal VatAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GrossValue { get; set; }
    }
}
