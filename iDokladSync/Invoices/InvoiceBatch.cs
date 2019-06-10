using System.Collections.Generic;
using IdokladSdk.ApiModels;

namespace iDokladSync.Invoices
{
    public class InvoiceBatch
    {
        public IList<InvoicePair> Invoices { get; set; } = new List<InvoicePair>();
        public int UpdatedContactsCount { get; private set; }
        public int NewContactsCount { get; private  set; }
        public int UpdatedInvoicesCount { get; private set; }
        public int NewInvoicesCount { get; private set; }


        public void ReportContactUpdate(Contact contact)
        {
            UpdatedContactsCount++;
        }

        public void ReportNewContact(Contact contact)
        {
            NewContactsCount++;
        }

        public void ReportUpdatedInvoice(IssuedInvoiceBase invoice)
        {
            UpdatedInvoicesCount++;
        }

        public void ReportNewInvoice(IssuedInvoiceBase invoice)
        {
            NewInvoicesCount++;
        }
    }
}