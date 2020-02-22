using Invoicing.Core.Database.Entities;
using System.Text;

namespace Invoicing.BusinessLogic.Factories.Templates
{
    public static class TemplateGenerator
    {
        public static string GetSaleInvoiceTemplate(Invoice invoice)
        {
            var sb = new StringBuilder();
			var index = 1;

			sb.AppendFormat(@"
                       <html>
						<head>
						</head>
						<body>
							<div class='header'>
								<h1>
								Faktura nr: {0}
								</h1>
							</div>
							<table align='center'>
								<tr>
									<th>Lp.</th>
									<th>Pozycja</th>
									<th>PKWiU</th>
									<th>Cena netto</th>
									<th>Ilość</th>
									<th>J.m.</th>
									<th>Wartość netto</th>
									<th>VAT%</th>
									<th>Kwota VAT</th>
									<th>Wartość brutto</th>
								</tr>", invoice.Number);
			foreach(var item in invoice.Details)
			{
				sb.AppendFormat(@"<tr>
									<td>{0}</td>
									<td>{1}</td>
									<td>{2}</td>
									<td>{3}</td>
									<td>{4}</td>
									<td>{5}</td>
									<td>{6}</td>
									<td>{7}</td>
									<td>{8}</td>
									<td>{9}</td>
								</tr>", index, item.ProductName, item.PKWiU, item.NetPrice, item.Quantity, item.Unit, item.NetValue, item.VatRate, item.VatAmount, item.GrossValue);
				index++;
			}
			sb.Append(@"
                                </table>
                            </body>
                        </html>");

			return sb.ToString();
        }
    }
}
