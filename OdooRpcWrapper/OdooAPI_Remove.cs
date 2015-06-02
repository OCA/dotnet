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
        public void Remove<T>(IEnumerable<T> records)
        {
            CheckLogin();
            CheckModel(typeof(T));

            string model = OdooObjectAttribute.GetModelName(typeof(T));
            List<int> ids = new List<int>();

            foreach (Object record in records)
            {               
                foreach (PropertyInfo property in record.GetType().GetProperties())
                {
                    if (OdooAttribute.HasOdooAttribute(property))
                    {
                        string fieldName = OdooAttribute.GetFieldName(property);

                        if (fieldName == STR_Id)
                        {
                            object rawValue = property.GetValue(record);

                            if (rawValue != null)
                            {
                                ids.Add(Int32.Parse(rawValue.ToString()));
                            }
                        }
                    }
                }
            }

            _objectRpc.unlink(_credentials.DbName, _credentials.UserId,
                _credentials.DbPassword, model, "unlink", ids.ToArray());
        }
    }
}
