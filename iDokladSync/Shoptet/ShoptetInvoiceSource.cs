using iDokladSync.Invoices;
using iDokladSync.Shoptet.Models;

namespace iDokladSync.Shoptet
{
    public abstract class ShoptetInvoiceSource : InvoiceSource
    {
        public dataPack Content => ContentRaw as dataPack;
    }
}