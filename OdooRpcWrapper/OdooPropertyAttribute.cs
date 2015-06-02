using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
