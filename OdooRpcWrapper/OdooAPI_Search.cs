using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OdooRpcWrapper
{
    public partial class OdooAPI
    {
        public int[] Search<T>(object[] filter)
        {
            Type type = typeof(T);

            CheckLogin();
            CheckModel(type);

            string model = OdooObjectAttribute.GetModelName(type);

            return _objectRpc.search(_credentials.DbName, _credentials.UserId,
                                     _credentials.DbPassword, model, "search", filter);
        }
    }
}
