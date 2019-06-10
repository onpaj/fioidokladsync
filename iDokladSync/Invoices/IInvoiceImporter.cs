namespace iDokladSync.Invoices
{
    public interface IInvoiceImporter
    {
        InvoiceBatch InvoiceBatch { get; }

        InvoiceBatch Load(InvoiceSource invoiceSource);

        InvoiceBatch Submit();

        InvoiceBatch SubmitInvoices();

        InvoiceBatch SubmitContacts();
    }
}