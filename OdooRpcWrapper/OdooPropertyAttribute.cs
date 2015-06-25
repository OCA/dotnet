using System;

namespace OdooRpcWrapper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OdooPropertyAttribute : OdooAttribute
    {
        public OdooPropertyAttribute(String name)
            : base(name)
        {

        }
    }
}
