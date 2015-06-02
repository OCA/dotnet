using OdooRpcWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.TypedTestClasses
{
    [OdooObject("res.users")]
    class GoodUser
    {
        [OdooProperty("id")]
        public int Id { get; set; }

        [OdooProperty("login")]
        public string Login { get; set; }
    }
}
