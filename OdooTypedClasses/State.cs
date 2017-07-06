using OdooRpcWrapper;

namespace OdooTypedClasses
{
    [OdooObject("res.country.state")]
    public class State
    {
        [OdooProperty("id")]
        public int Id { get; set; }

        [OdooProperty("name")]
        public string Name { get; set; }
    }
}
