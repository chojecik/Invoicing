using Invoicing.BusinessLogic.Helpers;
using Invoicing.Core.Database.Entities;
using System.Globalization;
using System.Text;

namespace Invoicing.BusinessLogic.Factories.Templates
{
    public static class TemplateGenerator
    {
        public static string GetSaleInvoiceTemplate(Invoice invoice, User user)
        {
            var sb = new StringBuilder();
			var index = 1;
			var numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
			numberFormatInfo.NumberGroupSeparator = " ";
			numberFormatInfo.NumberDecimalSeparator = ",";

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
							<div>
								<div class='dates-info'>
									<p>data wystawienia:	{15}</p>
									<p>data wydania towaru lub wykonania usługi:	{16}</p>
								</div>
							</div>
							<br>
							<br>
							<br>
							<br>
							<div class='row'>
								<div class='seller-info'>
									<h3>Wystawca:</h3>
									<p>{1}</p>
									<p>ul.{2} {3} {4}</p>
									<p>{5} {6}</p>
									<p>NIP: {7}</p>
								</div>
								<div class='buyer-info'>
									<h3>Nabywca:</h3>
									<p>{8}</p>
									<p>ul.{9} {10} {11}</p>
									<p>{12} {13}</p>
									<p>NIP: {14}</p>
								</div>
							</div>
							<br>
							<br>
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
								</tr>", 
								invoice.Number, 
								user.CompanyName, 
								user.Street, 
								user.HouseNumber, 
								user.LocalNumber, 
								user.ZipCode, 
								user.City, 
								user.Nip.DisplayNipFormat(), 
								invoice.Contractor.Name, 
								invoice.Contractor.Street, 
								invoice.Contractor.HouseNumber, 
								invoice.Contractor.LocalNumber, 
								invoice.Contractor.ZipCode, 
								invoice.Contractor.City, 
								invoice.Contractor.Nip.DisplayNipFormat(),
								invoice.DateOfIssue.ToString("dd/MM/yyyy"),
								invoice.DateOfService.ToString("dd/MM/yyyy"));
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
								</tr>", 
								index, 
								item.ProductName, 
								item.PKWiU, 
								item.NetPrice.ToString("#,0.00",numberFormatInfo), 
								item.Quantity, 
								item.Unit, 
								item.NetValue.ToString("#,0.00", numberFormatInfo), 
								item.VatRate, 
								item.VatAmount.ToString("#,0.00", numberFormatInfo), 
								item.GrossValue.ToString("#,0.00", numberFormatInfo));
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
