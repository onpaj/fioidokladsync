using iDokladSync.Invoices;
using iDokladSync.Shoptet;

namespace ImportHebloMVC.Controllers
{
    public class ShoptetInvoiceSourceFactory : IInvoiceSourceFactory
    {
        public InvoiceSource Create(string contentXml)
        {
            return new ShoptetXmlInvoiceSource(contentXml);
        }
    }
}