namespace iDokladSync.Shoptet.Models
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.stormware.cz/schema/version_2/invoice.xsd")]
    public partial class invoiceInvoiceItemHomeCurrency
    {

        private ushort unitPriceField;

        private ushort priceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public ushort unitPrice
        {
            get { return this.unitPriceField; }
            set { this.unitPriceField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
        public ushort price
        {
            get { return this.priceField; }
            set { this.priceField = value; }
        }
    }
}