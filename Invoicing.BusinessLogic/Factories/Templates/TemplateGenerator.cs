using Invoicing.BusinessLogic.Helpers;
using Invoicing.Core.Database.Entities;
using System.Globalization;
using System.Text;
using LiczbyNaSlowaNETCore;

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
									<table align='right' class='borderless noBorder'>
										<tr class='noBorder'>
											<td class='bold noBorder' align='right'>Data wystawienia:</td>
											<td class='noBorder' align='right'>{15}</td>
										</tr>
										<tr class='noBorder'>
											<td class='bold noBorder' align='right'>Data wydania towaru lub wykonania usługi:</td>
											<td class='noBorder' align='right'>{16}</td>
										</tr>
									</table>
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
								invoice.DateOfIssue.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
								invoice.DateOfService.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
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

			sb.AppendFormat(@"
								<tr>
									<td colspan='3' class='hidden'></td>
									<td colspan='3' class='highlighted bold'>Razem</td>
									<td class='highlighted'>{0}</td>
									<td class='highlighted'>-</td>
									<td class='highlighted'>{1}</td>
									<td class='highlighted'>{2}</td>
								</tr>
								<tr>
									<td colspan='3' class='hidden'></td>
									<td colspan='3' class='highlighted bold'>Rozliczenie VAT (PLN)</td>
									<td>{3}</td>
									<td>{4}</td>
									<td>{5}</td>
									<td>{6}</td>
								</tr>
							</table>",
								invoice.NetValue.ToString("#,0.00", numberFormatInfo),
								invoice.VatAmount.ToString("#,0.00", numberFormatInfo),
								invoice.GrossValue.ToString("#,0.00", numberFormatInfo),
								invoice.NetValue.ToString("#,0.00", numberFormatInfo),
								23,
								invoice.VatAmount.ToString("#,0.00", numberFormatInfo),
								invoice.GrossValue.ToString("#,0.00", numberFormatInfo));

			sb.AppendFormat(@"<br>
								<br>
								<br>
								<br>
								<div>
									<table align='right' class='borderless noBorder'>
										<tr class='noBorder'>
											<td class='bold noBorder' align='left'>Do zapłaty:</td>
											<td class='noBorder' align='right'>{0}</td>
										</tr>
										<tr class='noBorder'>
											<td class='bold noBorder' align='left'>Słownie:</td>
											<td class='noBorder' align='right'>{1}</td>
										</tr>
										<tr class='noBorder'>
											<td class='bold noBorder' align='left'>Sposób zapłaty:</td>
											<td class='noBorder' align='right'>przelew</td>
										</tr>
										<tr class='noBorder'>
											<td class='bold noBorder' align='left'>Termin:</td>
											<td class='noBorder' align='right'>{2}</td>
										</tr>
										<tr class='noBorder'>
											<td class='bold noBorder' align='left'>Rachunek:</td>
											<td class='noBorder' align='right'>{3}</td>
										</tr>
									</table>
								</div>",
								$"{invoice.GrossValue.ToString("#,0.00", numberFormatInfo)} PLN",
								NumberToText.Convert(invoice.GrossValue, new NumberToTextOptions {Currency = Currency.PLN, Stems = true }).DisplayDecimalValues(invoice.GrossValue),
								invoice.DateOfPayment.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
								user.BankAccount.DisplayBankAccountFormat());

			sb.Append(@"
                                
                            </body>
                        </html>");

			return sb.ToString();
        }
    }
}
