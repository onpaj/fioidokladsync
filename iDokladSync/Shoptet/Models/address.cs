namespace iDokladSync.Shoptet.Models
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd",
        IsNullable = false)]
    public partial class address
    {

        private string companyField;

        private string nameField;

        private string cityField;

        private string streetField;

        private string zipField;

        private addressCountry countryField;

        private object icoField;

        private object dicField;

        /// <remarks/>
        public string company
        {
            get { return this.companyField; }
            set { this.companyField = value; }
        }

        /// <remarks/>
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        /// <remarks/>
        public string city
        {
            get { return this.cityField; }
            set { this.cityField = value; }
        }

        /// <remarks/>
        public string street
        {
            get { return this.streetField; }
            set { this.streetField = value; }
        }

        /// <remarks/>
        public string zip
        {
            get { return this.zipField; }
            set { this.zipField = value; }
        }

        /// <remarks/>
        public addressCountry country
        {
            get { return this.countryField; }
            set { this.countryField = value; }
        }

        /// <remarks/>
        public object ico
        {
            get { return this.icoField; }
            set { this.icoField = value; }
        }

        /// <remarks/>
        public object dic
        {
            get { return this.dicField; }
            set { this.dicField = value; }
        }
    }
}