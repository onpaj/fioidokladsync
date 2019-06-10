using System;
using System.Collections.Generic;
using IdokladSdk.ApiFilters;
using IdokladSdk.ApiModels;

namespace iDokladSync.Invoices
{
    public interface IInvoiceParser
    {
        InvoiceBatch LoadInvoices(InvoiceSource invoiceSource, Func<ApiFilter, IList<IssuedInvoice>> prefetchInvoices = null, Func<ApiFilter, IList<Contact>> prefetchContacts = null);
    }
}