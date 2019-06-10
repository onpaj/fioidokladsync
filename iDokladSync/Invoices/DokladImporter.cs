using System.Collections.Generic;
using IdokladSdk;
using IdokladSdk.ApiFilters;
using IdokladSdk.ApiModels;

namespace iDokladSync.Invoices
{
    public class InvoiceImporter: IInvoiceImporter
    {
        private readonly ApiExplorer _idokladClient;
        private readonly IInvoiceParser _invoiceParser;


        public InvoiceImporter(ApiExplorer idokladClient, IInvoiceParser invoiceParser)
        {
            _idokladClient = idokladClient;
            _invoiceParser = invoiceParser;
        }


        public InvoiceBatch InvoiceBatch { get; private set; }


        public InvoiceBatch Load(InvoiceSource invoiceSource)
        {
            InvoiceBatch = _invoiceParser.LoadInvoices(invoiceSource, PrefetchInvoices, PrefetchContacts);

            return InvoiceBatch;
        }

        public InvoiceBatch Submit()
        {
            var updatedBatch = SubmitContacts();
            updatedBatch = SubmitInvoices();

            return InvoiceBatch;
        }

        public InvoiceBatch SubmitInvoices()
        {
            foreach (var pair in InvoiceBatch.Invoices)
            {
                if (pair.Invoice is IssuedInvoiceUpdate updatedInvoice)
                {
                    pair.Invoice = _idokladClient.IssuedInvoices.Update(updatedInvoice.Id, updatedInvoice);
                    InvoiceBatch.ReportUpdatedInvoice(pair.Invoice);
                }
                else
                {
                    pair.Invoice = _idokladClient.IssuedInvoices.Create(pair.Invoice as IssuedInvoiceCreate);
                    InvoiceBatch.ReportNewInvoice(pair.Invoice);
                }
            }

            return InvoiceBatch;
        }

        public InvoiceBatch SubmitContacts()
        {
            foreach (var pair in InvoiceBatch.Invoices)
            {
                if (pair.Contact.Id > 0)
                {
                    pair.Contact = _idokladClient.Contacts.Update(pair.Contact.Id, pair.Contact);
                    InvoiceBatch.ReportContactUpdate(pair.Contact);
                }
                else
                {
                    pair.Contact = _idokladClient.Contacts.Create(pair.Contact);
                    InvoiceBatch.ReportNewContact(pair.Contact);
                }
            }

            return InvoiceBatch;
        }


        private IList<IssuedInvoice> PrefetchInvoices(ApiFilter filter)
        {
            return _idokladClient.IssuedInvoices.IssuedInvoices(filter).Data;
        }

        private IList<Contact> PrefetchContacts(ApiFilter filter)
        {
            return _idokladClient.Contacts.Contacts(filter).Data;
        }
    }
}