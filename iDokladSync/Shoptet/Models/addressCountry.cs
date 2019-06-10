namespace iDokladSync.Shoptet.Models
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
    public partial class addressCountry
    {

        private string idsField;

        /// <remarks/>
        public string ids
        {
            get { return this.idsField; }
            set { this.idsField = value; }
        }
    }
}