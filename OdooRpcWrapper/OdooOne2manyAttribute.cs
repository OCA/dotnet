using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;

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
