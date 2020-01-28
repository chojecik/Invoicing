using Invoicing.Core.Enums;
using System;

namespace Invoicing.Web.Models
{
    public class InvoiceModel
    {
        public string InvoiceNumber { get; set; }
        public string Contractor { get; set; }
        public DateTime Date { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VatAmount { get; set; }
        public InvoiceType Type { get; set; }
        public string FilePath { get; set; }
    }
}
