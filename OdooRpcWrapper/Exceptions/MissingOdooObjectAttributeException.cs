using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace OdooRpcWrapper.Exceptions
{
    public class MissingOdooObjectAttributeException : InvalidOperationException
    {
        public MissingOdooObjectAttributeException()
            : base("No OdooObject attribute found on class!")
        {
            
        }
    }
}
