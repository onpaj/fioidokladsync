namespace iDokladSync.Shoptet
{
    public class ShoptetXmlInvoiceSource : ShoptetInvoiceSource
    {
        public ShoptetXmlInvoiceSource(string xml)
        {
            var serializer = new ShoptetSerializer();
            ContentRaw = serializer.Deserialize(xml);
        }
    }
}