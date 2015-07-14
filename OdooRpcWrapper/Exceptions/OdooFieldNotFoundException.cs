using System;

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
