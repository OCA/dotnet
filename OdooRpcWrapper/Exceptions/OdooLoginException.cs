using System;

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
