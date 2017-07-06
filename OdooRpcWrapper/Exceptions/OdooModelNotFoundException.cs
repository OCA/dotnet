using System;

namespace OdooRpcWrapper.Exceptions
{
    public class OdooModelNotFoundException : InvalidOperationException
    {
        public OdooModelNotFoundException(string modelName)
            : base(String.Format("{0} {1} {2}", "Odoo model ", modelName, " was not found!"))
        {
            
        }
    }
}
