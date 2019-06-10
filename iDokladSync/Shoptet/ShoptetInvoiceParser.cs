using System;
using System.Collections.Generic;
using System.Linq;
using iDokladSync.Invoices;
using iDokladSync.Shoptet.Models;
using IdokladSdk.ApiFilters;
using IdokladSdk.ApiModels;

namespace iDokladSync.Shoptet
{
    public class ShoptetInvoiceParser : IInvoiceParser
    {
        private IList<Contact> _prefetchedContacts;
        private IList<IssuedInvoice> _prefetchedInvoices;

        public InvoiceBatch LoadInvoices(
            InvoiceSource invoiceSource, 
            Func<ApiFilter, IList<IssuedInvoice>> invoicePrefetch = null, 
            Func<ApiFilter, IList<Contact>> contactPrefetch = null)
        {
            var data = (invoiceSource as ShoptetInvoiceSource)?.Content ?? throw new InvalidCastException($"{nameof(invoiceSource)} has to be of type {nameof(ShoptetInvoiceSource)}");
            
            _prefetchedContacts = contactPrefetch?.Invoke(GetContactFilter(data)) ?? new List<Contact>();
            _prefetchedInvoices = invoicePrefetch?.Invoke(GetInvoiceFilter(data)) ?? new List<IssuedInvoice>();

            var batch = new InvoiceBatch();

            foreach (var i in data.dataPackItem)
            {
                var contact = ParseContact(i);
                var invoice = ParseInvoice(i, contact);

                batch.Invoices.Add(new InvoicePair(invoice, contact));
            }

            return batch;
        }


        private IssuedInvoiceBase ParseInvoice(dataPackDataPackItem i, Contact contact)
        {
            var invoice = MatchInvoice(i.invoice.invoiceHeader.number.numberRequested.ToString());

            if (invoice != null)
            {
                return MergeInvoice(i.invoice, invoice);
            }
            else
            {
                return CreateInvoice(i.invoice, contact.Id);
            }
        }

        private Contact ParseContact(dataPackDataPackItem i)
        {
            var customer = MatchContact(i.invoice.invoiceHeader.partnerIdentity.address);
            customer = MergeContact(i.invoice.invoiceHeader.partnerIdentity.address, customer);

            return customer;
        }


        private ApiFilter GetInvoiceFilter(dataPack data)
        {
            var filter = new ApiFilter(filterType: FilterType.Or);

            var ids = data.dataPackItem?.Select(s => s.invoice.invoiceHeader.number.numberRequested.ToString());

           
            filter.Filters = new HashSet<FilterItem>(ids.Select(s =>
            {
                var f = new FilterItem("DocumentNumber");
                f.Set(FilterOperator.Eq, s);
                return f;
            }));

            return filter.WithPaging(1, int.MaxValue);
        }


        private ApiFilter GetContactFilter(dataPack data)
        {
            return new ApiFilter().WithPaging(1, int.MaxValue);
        }


        private IssuedInvoiceUpdate MergeInvoice(invoice invoice, IssuedInvoice existingInvoice)
        {
            var updatedInvoice = AutoMapper.Mapper.Map<IssuedInvoiceUpdate>(existingInvoice);

            updatedInvoice.DateOfIssue = invoice.invoiceHeader.date;
            updatedInvoice.VariableSymbol = invoice.invoiceHeader.symVar.ToString();

            updatedInvoice.IssuedInvoiceItems = GetUpdateItems(invoice.invoiceDetail);

            return updatedInvoice;
        }

        private IssuedInvoice MatchInvoice(string documentNumber)
        {
            return _prefetchedInvoices.FirstOrDefault(f => f.DocumentNumber == documentNumber);
        }


        private List<IssuedInvoiceItemUpdate> GetUpdateItems(invoiceInvoiceItem[] invoiceInvoiceDetail)
        {
            var items = new List<IssuedInvoiceItemUpdate>();

            foreach (var i in invoiceInvoiceDetail)
            {
                items.Add(new IssuedInvoiceItemUpdate
                {
                    Amount = i.quantity,
                    Name = i.text,
                    UnitPrice = i.homeCurrency.unitPrice,
                });
            }

            return items;
        }



        private List<IssuedInvoiceItem> GetItems(invoiceInvoiceItem[] invoiceInvoiceDetail)
        {
            var items = new List<IssuedInvoiceItem>();

            foreach (var i in invoiceInvoiceDetail)
            {
                items.Add(new IssuedInvoiceItem
                {
                    Amount = i.quantity,
                    Code = i.code,
                    Name = i.text,
                    Price = i.homeCurrency.price,
                    UnitPrice = i.homeCurrency.unitPrice,
                });
            }

            return items;
        }

        private IssuedInvoiceCreate CreateInvoice(invoice i, int partnerID)
        {
            var invoice = new IssuedInvoiceCreate
            {
                DocumentNumber = i.invoiceHeader.number.numberRequested.ToString(),
                DateOfIssue = i.invoiceHeader.date,
                VariableSymbol = i.invoiceHeader.symVar.ToString(),
                OrderNumber = i.invoiceHeader.numberOrder,
                IssuedInvoiceItems = GetItems(i.invoiceDetail),
                PurchaserId = partnerID,

                DocumentSerialNumber = Convert.ToInt32(i.invoiceHeader.number.numberRequested.ToString().Substring(4)) // strip year
            };

            return invoice;
        }


        private Contact MatchContact(address c)
        {
            return _prefetchedContacts.FirstOrDefault(f => HasSameICO(c, f) || HasSameName(c, f));
        }

        private static bool HasSameName(address c, Contact f)
        {
            return c.name == f.Firstname + " " + f.Surname;
        }

        private static bool HasSameICO(address c, Contact f)
        {
            return (c.ico is string && ((string)c.ico).Length > 0 && ((string)c.ico) == f.IdentificationNumber);
        }

        private Contact MergeContact(address c, Contact customer)
        {
            var nameSplit = c.name.Split(' ');

            if (customer == null)
                customer = new Contact();

            customer.CompanyName = c.company.Length > 0 ? c.company : c.name;
            customer.Firstname = nameSplit.Length == 2 ? nameSplit[0] : "";
            customer.Surname = nameSplit.Length == 2 ? nameSplit[1] : "";
            customer.City = c.city;
            customer.Street = c.street;
            customer.IdentificationNumber = c.ico is string ? c.ico.ToString() : "";
            customer.VatIdentificationNumber = c.dic is string ? c.dic.ToString() : "";
            customer.PostalCode = c.zip;
            customer.CountryId = GetCountryId(c.country.ids);

            return customer;
        }

        private int GetCountryId(string countryIds)
        {
            if (countryIds.StartsWith("sk"))
                return 1;

            return 2; //CZ
        }
    }
}