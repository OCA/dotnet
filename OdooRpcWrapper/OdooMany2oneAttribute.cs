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
    public class OdooMany2oneAttribute : OdooAttribute
    {
        public OdooMany2oneAttribute(String name)
            : base(name)
        {

        }
    }
}
