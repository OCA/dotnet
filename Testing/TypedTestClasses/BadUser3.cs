using OdooRpcWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.TypedTestClasses
{
    [OdooObject("res.users")]
    class BadUser3
    {
        [OdooProperty("id")]
        public int Id { get; set; }

        [OdooProperty("badprop")]
        public string Name { get; set; }
    }
}
