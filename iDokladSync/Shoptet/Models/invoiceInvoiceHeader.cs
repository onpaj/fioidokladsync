namespace iDokladSync.Shoptet.Models
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.stormware.cz/schema/version_2/invoice.xsd")]
    public partial class invoiceInvoiceHeader
    {

        private string invoiceTypeField;

        private invoiceInvoiceHeaderNumber numberField;

        private invoiceInvoiceHeaderPaymentType paymentTypeField;

        private invoiceInvoiceHeaderCarrier carrierField;

        private string numberOrderField;

        private uint symVarField;

        private System.DateTime dateField;

        private System.DateTime dateTaxField;

        private System.DateTime dateDueField;

        private invoiceInvoiceHeaderPartnerIdentity partnerIdentityField;

        /// <remarks/>
        public string invoiceType
        {
            get { return this.invoiceTypeField; }
            set { this.invoiceTypeField = value; }
        }

        /// <remarks/>
        public invoiceInvoiceHeaderNumber number
        {
            get { return this.numberField; }
            set { this.numberField = value; }
        }

        /// <remarks/>
        public invoiceInvoiceHeaderPaymentType paymentType
        {
            get { return this.paymentTypeField; }
            set { this.paymentTypeField = value; }
        }

        /// <remarks/>
        public invoiceInvoiceHeaderCarrier carrier
        {
            get { return this.carrierField; }
            set { this.carrierField = value; }
        }

        /// <remarks/>
        public string numberOrder
        {
            get { return this.numberOrderField; }
            set { this.numberOrderField = value; }
        }

        /// <remarks/>
        public uint symVar
        {
            get { return this.symVarField; }
            set { this.symVarField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime date
        {
            get { return this.dateField; }
            set { this.dateField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime dateTax
        {
            get { return this.dateTaxField; }
            set { this.dateTaxField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime dateDue
        {
            get { return this.dateDueField; }
            set { this.dateDueField = value; }
        }

        /// <remarks/>
        public invoiceInvoiceHeaderPartnerIdentity partnerIdentity
        {
            get { return this.partnerIdentityField; }
            set { this.partnerIdentityField = value; }
        }
    }
}