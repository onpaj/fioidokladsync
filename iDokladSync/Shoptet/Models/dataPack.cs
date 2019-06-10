using System;
using System.Collections.Generic;
using System.Text;

namespace iDokladSync.Shoptet.Models
{
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.stormware.cz/schema/version_2/data.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.stormware.cz/schema/version_2/data.xsd",
        IsNullable = false)]
    public partial class dataPack
    {

        private dataPackDataPackItem[] dataPackItemField;

        private string idField;

        private uint icoField;

        private string applicationField;

        private decimal versionField;

        private string noteField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("dataPackItem")]
        public dataPackDataPackItem[] dataPackItem
        {
            get { return this.dataPackItemField; }
            set { this.dataPackItemField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get { return this.idField; }
            set { this.idField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint ico
        {
            get { return this.icoField; }
            set { this.icoField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string application
        {
            get { return this.applicationField; }
            set { this.applicationField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get { return this.versionField; }
            set { this.versionField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string note
        {
            get { return this.noteField; }
            set { this.noteField = value; }
        }
    }
}