using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace OdooRpcWrapper
{
    public partial class OdooAPI
    {
        public void Write<T>(IEnumerable<T> records)
        {
            CheckLogin();
            CheckModel(typeof(T));
            string model = OdooObjectAttribute.GetModelName(typeof(T));


            foreach (Object record in records)
            {
                if (OdooObjectAttribute.IsOdooObject(record))
                {
                    int id = 0;
                    XmlRpcStruct values = new XmlRpcStruct();

                    foreach (PropertyInfo property in record.GetType().GetProperties())
                    {
                        if (OdooAttribute.HasOdooPropertyAttribute(property))
                        {
                            string fieldName = OdooPropertyAttribute.GetFieldName(property);

                            if (fieldName != STR_Id)
                            {
                                object rawValue = property.GetValue(record);

                                // TODO: Extra checks on value? e.g. String == "False"
                                if (rawValue != null)
                                {
                                    values[fieldName] = rawValue;
                                }
                            }
                            else
                            {
                                id = Int32.Parse(property.GetValue(record).ToString());
                            }
                        }
                        else if (OdooAttribute.HasOdooMany2oneAttribute(property))
                        {
                            string fieldName = OdooMany2oneAttribute.GetFieldName(property);
                            object rawValue = property.GetValue(record);

                            if (rawValue != null)
                            {
                                // search for id property on one2many
                                foreach (PropertyInfo p in rawValue.GetType().GetProperties())
                                {
                                    if (OdooAttribute.HasOdooAttribute(p))
                                    {
                                        string fn = OdooPropertyAttribute.GetFieldName(property);

                                        if (fn == STR_Id)
                                        {
                                            object rv = p.GetValue(record);

                                            if (rv != null)
                                            {
                                                values[fieldName] = rawValue.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    _objectRpc.write(_credentials.DbName, _credentials.UserId,
                                            _credentials.DbPassword, model, "write", new int[1] { id }, values);
                }
            }
        }
    }
}
