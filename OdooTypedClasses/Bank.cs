using OdooRpcWrapper;

namespace OdooTypedClasses
{
    [OdooObject("res.partner.bank")]
    public class Bank
    {
        [OdooProperty("id")]
        public int Id { get; set; }

        [OdooProperty("name")]
        public string Name { get; set; }

        [OdooProperty("acc_number")]
        public string AccountNumber { get; set; }

        [OdooProperty("state")]
        public string State { get; set; }
    }
}
