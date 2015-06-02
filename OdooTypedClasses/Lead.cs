using OdooRpcWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdooTypedClasses
{
    [OdooObject("crm.lead")]
    public class Lead
    {
        [OdooProperty("id")]
        public int Id { get; set; }

        [OdooProperty("name")]
        public string Name { get; set; }

        [OdooProperty("type")]
        public string Type { get; set; }

        [OdooMany2one("user_id")]
        public User SalesPerson { get; set; }
    }
}
