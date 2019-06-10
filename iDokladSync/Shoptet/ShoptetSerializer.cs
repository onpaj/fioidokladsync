using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using iDokladSync.Shoptet.Models;

namespace iDokladSync.Shoptet
{
    public class ShoptetSerializer
    {
        public dataPack Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(dataPack));
            using (TextReader reader = new StringReader(xml))
            {
                dataPack result = (dataPack)serializer.Deserialize(reader);
                return result;

            }
        }
    }
}
