namespace iDokladSync.Shoptet.Models
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.stormware.cz/schema/version_2/data.xsd")]
    public partial class dataPackDataPackItem
    {

        private invoice invoiceField;

        private uint idField;

        private decimal versionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace =
            "http://www.stormware.cz/schema/version_2/invoice.xsd")]
        public invoice invoice
        {
            get { return this.invoiceField; }
            set { this.invoiceField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint id
        {
            get { return this.idField; }
            set { this.idField = value; }
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