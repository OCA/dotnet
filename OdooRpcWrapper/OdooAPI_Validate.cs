using CookComputing.XmlRpc;
using OdooRpcWrapper.Exceptions;
using System;
using System.Reflection;

namespace OdooRpcWrapper
{
    public partial class OdooAPI
    {
        public bool ValidateModelType(Type type)
        {
            CheckLogin();

            if(! OdooObjectAttribute.IsOdooObject(type))
            {
                throw new MissingOdooObjectAttributeException();
            }

            string modelName = OdooObjectAttribute.GetModelName(type);
            object[] filter = new object[1];
            filter[0] = new object[3] { "model", "=", modelName };

            int[] result = _objectRpc.search(_credentials.DbName, _credentials.UserId,
                                     _credentials.DbPassword, "ir.model", "search", filter);

            if(result.Length == 0)
            {
                throw new OdooModelNotFoundException(modelName);
            }

            filter[0] = new object[3] { "model_id", "=", result[0] };
            result = _objectRpc.search(_credentials.DbName, _credentials.UserId,
                                     _credentials.DbPassword, "ir.model.fields", "search", filter);

            object[] fields = _objectRpc.read(_credentials.DbName, _credentials.UserId,
                                     _credentials.DbPassword, "ir.model.fields", "read", 
                                     result, new string[1]{ "name" });

            foreach(PropertyInfo property in type.GetProperties())
            {
                if(OdooAttribute.HasOdooAttribute(property))
                {
                    string fieldName = OdooAttribute.GetFieldName(property);
                    bool found = false;

                    foreach(object rec in fields)
                    {
                        XmlRpcStruct vals = (XmlRpcStruct)rec;
                        if(vals["name"].ToString() == fieldName)
                        {
                            found = true;
                            break;
                        }
                    }

                    if(!found)
                    {
                        throw new OdooFieldNotFoundException(fieldName);
                    }
                }
            }

            return true;
        }
    }
}
