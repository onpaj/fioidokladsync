namespace iDokladSync.Shoptet.Models
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.stormware.cz/schema/version_2/invoice.xsd")]
    public partial class invoiceInvoiceItem
    {

        private string textField;

        private byte quantityField;

        private string unitField;

        private bool payVATField;

        private invoiceInvoiceItemHomeCurrency homeCurrencyField;

        private invoiceInvoiceItemStockItem stockItemField;

        private string codeField;

        /// <remarks/>
        public string text
        {
            get { return this.textField; }
            set { this.textField = value; }
        }

        /// <remarks/>
        public byte quantity
        {
            get { return this.quantityField; }
            set { this.quantityField = value; }
        }

        /// <remarks/>
        public string unit
        {
            get { return this.unitField; }
            set { this.unitField = value; }
        }

        /// <remarks/>
        public bool payVAT
        {
            get { return this.payVATField; }
            set { this.payVATField = value; }
        }

        /// <remarks/>
        public invoiceInvoiceItemHomeCurrency homeCurrency
        {
            get { return this.homeCurrencyField; }
            set { this.homeCurrencyField = value; }
        }

        /// <remarks/>
        public invoiceInvoiceItemStockItem stockItem
        {
            get { return this.stockItemField; }
            set { this.stockItemField = value; }
        }

        /// <remarks/>
        public string code
        {
            get { return this.codeField; }
            set { this.codeField = value; }
        }
    }
}