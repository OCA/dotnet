using CookComputing.XmlRpc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OdooRpcWrapper
{
    public partial class OdooAPI
    {
        public IList<T> Read<T>(int[] ids)
        {
            CheckLogin();
            CheckModel(typeof(T));

            Type type = typeof(T);
            List<string> fields = new List<string>();
            List<T> result = new List<T>();


            string model = OdooObjectAttribute.GetModelName(type);

            foreach (PropertyInfo property in type.GetProperties())
            {
                if(OdooAttribute.HasOdooAttribute(property))
                {
                    string fieldName = OdooAttribute.GetFieldName(property);
                    fields.Add(fieldName);
                }
            }

            object[] records = _objectRpc.read(_credentials.DbName, _credentials.UserId,
                                _credentials.DbPassword, model, "read", ids, fields.ToArray());

            foreach (object rec in records)
            {
                XmlRpcStruct vals = (XmlRpcStruct)rec;

                T obj = (T)Activator.CreateInstance(type);

                foreach (PropertyInfo property in type.GetProperties())
                {
                    if (OdooAttribute.HasOdooPropertyAttribute(property))
                    {
                        string field = OdooPropertyAttribute.GetFieldName(property);

                        if (vals.ContainsKey(field))
                        {
                            object rawValue = vals[field];

                            if (rawValue is bool &&
                                !(property.PropertyType == typeof(bool) || property.PropertyType == typeof(Boolean)))
                            {
                                // property is false??

                            }
                            else
                            {
                                property.SetValue(obj, vals[field]);
                            }
                        }
                    }
                    else if (OdooAttribute.HasOdooMany2oneAttribute(property))
                    {
                        string field = OdooMany2oneAttribute.GetFieldName(property);

                        if (vals.ContainsKey(field))
                        {
                            object rawV = vals[field];
                            if (rawV is object[])
                            {
                                object[] rawVal = (object[])vals[field];
                                Type relationType = property.PropertyType;

                                if (OdooObjectAttribute.IsOdooObject(relationType))
                                {
                                    Object rel = Activator.CreateInstance(relationType);

                                    PropertyInfo[] props = rel.GetType().GetProperties();

                                    foreach (PropertyInfo prop in props)
                                    {
                                        if (OdooAttribute.HasOdooPropertyAttribute(prop))
                                        {
                                            string name2 = OdooPropertyAttribute.GetFieldName(prop);

                                            if (name2 == STR_Id)
                                            {
                                                prop.SetValue(rel, rawVal[0]);
                                            }
                                            else if (name2 == "name")
                                            {
                                                prop.SetValue(rel, rawVal[1]);
                                            }
                                        }
                                    }

                                    property.SetValue(obj, rel);
                                }
                            }
                        }
                    }
                    else if (OdooAttribute.HasOdooOne2manyAttribute(property))
                    {
                        string field = OdooOne2manyAttribute.GetFieldName(property);

                        if (vals.ContainsKey(field))
                        {
                            Int32[] rawVals = null;
                            if (vals[field] is object[])
                            {
                                rawVals = new Int32[0];
                            }
                            else
                            {
                                rawVals = (Int32[])vals[field];
                            }

                            Type relationType = property.PropertyType.GetGenericArguments()[0];
                            var listType = typeof(List<>);
                            var constructedListType = listType.MakeGenericType(relationType);
                            var instance = (IList)Activator.CreateInstance(constructedListType);

                            foreach (int id in rawVals)
                            {
                                Object rel = Activator.CreateInstance(relationType);

                                PropertyInfo[] props = rel.GetType().GetProperties();

                                foreach (PropertyInfo prop in props)
                                {
                                    if (OdooAttribute.HasOdooAttribute(prop))
                                    {
                                        string name2 = OdooPropertyAttribute.GetFieldName(prop);

                                        if (name2 == "id")
                                        {
                                            prop.SetValue(rel, id);
                                        }
                                    }
                                }

                                instance.Add(rel);
                            }
                            property.SetValue(obj, instance);
                        }
                    }
                }
                result.Add(obj);
            }
            
            return result;
        }

        public void Read<T>(IEnumerable<T> records)
        {
            CheckLogin();
            CheckModel(typeof(T));

            Type type = typeof(T);
            List<string> fields = new List<string>();
            List<T> result = new List<T>();


            string model = OdooObjectAttribute.GetModelName(type);

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (OdooAttribute.HasOdooAttribute(property))
                {
                    string fieldName = OdooAttribute.GetFieldName(property);
                    fields.Add(fieldName);
                }
            }

            List<int> ids = new List<int>();

            foreach (T record in records)
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

            object[] recs = _objectRpc.read(_credentials.DbName, _credentials.UserId,
                                _credentials.DbPassword, model, "read", ids.ToArray(), fields.ToArray());

            foreach (object rec in recs)
            {
                XmlRpcStruct vals = (XmlRpcStruct)rec;

                // search object with this id
                int id2 = Int32.Parse(vals["id"].ToString());
                T obj = (T)Activator.CreateInstance(type);

                foreach (T record in records)
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
                                    if(id2 == Int32.Parse(rawValue.ToString()))
                                    {
                                        // found!
                                        obj = record;
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (PropertyInfo property in type.GetProperties())
                {
                    if (OdooAttribute.HasOdooPropertyAttribute(property))
                    {
                        string field = OdooPropertyAttribute.GetFieldName(property);

                        if (vals.ContainsKey(field))
                        {
                            object rawValue = vals[field];

                            if (rawValue is bool &&
                                !(property.PropertyType == typeof(bool) || property.PropertyType == typeof(Boolean)))
                            {
                                // property is false??

                            }
                            else
                            {
                                property.SetValue(obj, vals[field]);
                            }
                        }
                    }
                    else if (OdooAttribute.HasOdooMany2oneAttribute(property))
                    {
                        string field = OdooMany2oneAttribute.GetFieldName(property);

                        if (vals.ContainsKey(field) && vals[field] is object[])
                        {                            
                            object[] rawVal = (object[])vals[field];
                            Type relationType = property.PropertyType;

                            if (OdooObjectAttribute.IsOdooObject(relationType))
                            {
                                Object rel = Activator.CreateInstance(relationType);

                                PropertyInfo[] props = rel.GetType().GetProperties();

                                foreach (PropertyInfo prop in props)
                                {
                                    if (OdooAttribute.HasOdooPropertyAttribute(prop))
                                    {
                                        string name2 = OdooPropertyAttribute.GetFieldName(prop);

                                        if (name2 == STR_Id)
                                        {
                                            prop.SetValue(rel, rawVal[0]);
                                        }
                                        else if (name2 == "name")
                                        {
                                            prop.SetValue(rel, rawVal[1]);
                                        }
                                    }
                                }

                                property.SetValue(obj, rel);
                            }
                        }
                    }
                    else if (OdooAttribute.HasOdooOne2manyAttribute(property))
                    {
                        string field = OdooOne2manyAttribute.GetFieldName(property);

                        if (vals.ContainsKey(field))
                        {
                            Int32[] rawVals = null;
                            if (vals[field] is object[])
                            {
                                rawVals = new Int32[0];
                            }
                            else
                            {
                                rawVals = (Int32[])vals[field];
                            }

                            Type relationType = property.PropertyType.GetGenericArguments()[0];
                            var listType = typeof(List<>);
                            var constructedListType = listType.MakeGenericType(relationType);
                            var instance = (IList)Activator.CreateInstance(constructedListType);

                            foreach (int id in rawVals)
                            {
                                Object rel = Activator.CreateInstance(relationType);

                                PropertyInfo[] props = rel.GetType().GetProperties();

                                foreach (PropertyInfo prop in props)
                                {
                                    if (OdooAttribute.HasOdooAttribute(prop))
                                    {
                                        string name2 = OdooPropertyAttribute.GetFieldName(prop);

                                        if (name2 == "id")
                                        {
                                            prop.SetValue(rel, id);
                                        }
                                    }
                                }

                                instance.Add(rel);
                            }
                            property.SetValue(obj, instance);
                        }
                    }
                }
                result.Add(obj);
            }
        }
    }
}
