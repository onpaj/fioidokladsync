namespace iDokladSync.Invoices
{
    public interface IInvoiceSource<T>
    {
        T Content { get; }
    }
}