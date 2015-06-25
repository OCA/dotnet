using System;

namespace OdooRpcWrapper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OdooMany2oneAttribute : OdooAttribute
    {
        public OdooMany2oneAttribute(String name)
            : base(name)
        {

        }
    }
}
