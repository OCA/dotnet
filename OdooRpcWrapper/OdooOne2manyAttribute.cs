using System;

namespace OdooRpcWrapper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OdooOne2manyAttribute : OdooAttribute
    {
        public OdooOne2manyAttribute(String name)
            : base(name)
        {

        }
    }
}
