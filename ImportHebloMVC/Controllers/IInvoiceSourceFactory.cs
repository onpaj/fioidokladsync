using iDokladSync.Invoices;

namespace ImportHebloMVC.Controllers
{
    public interface IInvoiceSourceFactory  
    {
        InvoiceSource Create(string contentXml);
    }
}