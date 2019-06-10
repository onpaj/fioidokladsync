namespace iDokladSync.Shoptet.Models
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.stormware.cz/schema/version_2/type.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.stormware.cz/schema/version_2/type.xsd",
        IsNullable = false)]
    public partial class round
    {

        private byte priceRoundField;

        /// <remarks/>
        public byte priceRound
        {
            get { return this.priceRoundField; }
            set { this.priceRoundField = value; }
        }
    }
}