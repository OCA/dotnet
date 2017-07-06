using OdooRpcWrapper;

namespace OdooTypedClasses
{
    [OdooObject("res.users")]
    public class User
    {
        [OdooProperty("id")]
        public int Id { get; set; }

        [OdooProperty("login")]
        public string Login { get; set; }

        [OdooMany2one("partner_id")]
        public Partner Partner { get; set; }

    }
}
