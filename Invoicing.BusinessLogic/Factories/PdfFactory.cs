using DinkToPdf;
using DinkToPdf.Contracts;
using Invoicing.Core.Database.Entities;
using System.IO;

namespace Invoicing.BusinessLogic.Factories
{
    public class PdfFactory
    {
        private readonly IConverter _converter;

        public PdfFactory(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GeneratePdf(Invoice invoice, string template)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = invoice.Number
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = template,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return file;
        }
    }
}
