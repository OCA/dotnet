
namespace OdooRpcWrapper
{
    [OdooObject("ir.model")]
    internal class OdooModel
    {
        [OdooProperty("id")]
        public int Id { get; set; }

        [OdooProperty("name")]
        public string Name { get; set; }

    }
}
