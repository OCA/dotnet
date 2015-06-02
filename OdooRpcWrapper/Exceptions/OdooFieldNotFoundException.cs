using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace OdooRpcWrapper.Exceptions
{
    public class OdooFieldNotFoundException : InvalidOperationException
    {
        public OdooFieldNotFoundException(string fieldName)
            : base(String.Format("{0} {1} {2}","Odoo field ",fieldName," was not found!"))
        {
            
        }
    }
}
