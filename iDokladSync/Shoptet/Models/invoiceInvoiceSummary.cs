namespace iDokladSync.Shoptet.Models
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.stormware.cz/schema/version_2/invoice.xsd")]
    public partial class invoiceInvoiceSummary
    {

        private string roundingDocumentField;

        private invoiceInvoiceSummaryHomeCurrency homeCurrencyField;

        /// <remarks/>
        public string roundingDocument
        {
            get { return this.roundingDocumentField; }
            set { this.roundingDocumentField = value; }
        }

        /// <remarks/>
        public invoiceInvoiceSummaryHomeCurrency homeCurrency
        {
            get { return this.homeCurrencyField; }
            set { this.homeCurrencyField = value; }
        }
    }
}