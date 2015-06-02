using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdooRpcWrapper.Exceptions
{
    public class OdooLoginException : InvalidOperationException
    {
        public OdooLoginException()
            : base("Not logged in to Odoo!")
        {

        }
    }
}
