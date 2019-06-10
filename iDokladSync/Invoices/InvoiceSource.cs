namespace iDokladSync.Invoices
{
    public abstract class InvoiceSource
    {
        public object ContentRaw { get; protected set; }
    }
}