using OdooRpcWrapper;
using System.Collections.Generic;

namespace OdooTypedClasses
{
    [OdooObject("res.partner")]
    public class Partner
    {
        [OdooProperty("id")]
        public int Id { get; set; }

        [OdooProperty("name")]
        public string Name { get; set; }

        [OdooProperty("street")]
        public string Street { get; set; }

        [OdooProperty("zip")]
        public string Zip { get; set; }

        [OdooProperty("city")]
        public string City { get; set; }

        [OdooProperty("vat")]
        public string Vat { get; set; }

        [OdooProperty("phone")]
        public string Phone{ get; set; }

        [OdooProperty("mobile")]
        public string Mobile { get; set; }

        [OdooProperty("fax")]
        public string Fax { get; set; }

        [OdooProperty("email")]
        public string Email { get; set; }

        [OdooProperty("customer")]
        public bool Customer { get; set; }

        [OdooProperty("supplier")]
        public bool Supplier { get; set; }

        [OdooMany2one("country_id")]
        public Country Country { get; set; }

        [OdooMany2one("state_id")]
        public State State { get; set; }

        [OdooOne2many("bank_ids")]
        public IList<Bank> Banks { get; set; }

    }
}
