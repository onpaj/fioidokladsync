namespace iDokladSync.Shoptet.Models
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.stormware.cz/schema/version_2/invoice.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.stormware.cz/schema/version_2/invoice.xsd",
        IsNullable = false)]
    public partial class invoice
    {

        private invoiceInvoiceHeader invoiceHeaderField;

        private invoiceInvoiceItem[] invoiceDetailField;

        private invoiceInvoiceSummary invoiceSummaryField;

        private decimal versionField;

        /// <remarks/>
        public invoiceInvoiceHeader invoiceHeader
        {
            get { return this.invoiceHeaderField; }
            set { this.invoiceHeaderField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("invoiceItem", IsNullable = false)]
        public invoiceInvoiceItem[] invoiceDetail
        {
            get { return this.invoiceDetailField; }
            set { this.invoiceDetailField = value; }
        }

        /// <remarks/>
        public invoiceInvoiceSummary invoiceSummary
        {
            get { return this.invoiceSummaryField; }
            set { this.invoiceSummaryField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get { return this.versionField; }
            set { this.versionField = value; }
        }
    }
}