using IdokladSdk.ApiModels;

namespace iDokladSync.Invoices
{
    public class InvoicePair
    {
        public InvoicePair(IssuedInvoiceBase invoice, Contact contact)
        {
            this.Invoice = invoice;
            this.Contact = contact;
        }

        public IssuedInvoiceBase Invoice { get; set; }
        public Contact Contact { get; set; }
    }
}