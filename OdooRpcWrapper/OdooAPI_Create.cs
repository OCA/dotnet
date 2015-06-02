using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OdooRpcWrapper
{
    public partial class OdooAPI
    {
        public void Create<T>(IEnumerable<T> records)
        {
            CheckLogin();
            CheckModel(typeof(T));

            foreach (Object record in records)
            {
                string model = OdooObjectAttribute.GetModelName(record);
                PropertyInfo idProperty = null;
                XmlRpcStruct values = new XmlRpcStruct();

                foreach (PropertyInfo property in record.GetType().GetProperties())
                {
                    if(OdooAttribute.HasOdooAttribute(property))
                    {
                        string fieldName = OdooAttribute.GetFieldName(property);
                                        
                        if (OdooAttribute.HasOdooPropertyAttribute(property))
                        {
                            if (fieldName != STR_Id)
                            {
                                object rawValue = property.GetValue(record);
                                if (rawValue != null)
                                {
                                    values[fieldName] = rawValue;
                                }
                            }
                            else
                            {
                                idProperty = property;
                            }
                        }
                        else if (OdooAttribute.HasOdooMany2oneAttribute(property))
                        {
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
                }

                int id = SafeCreate(model, values);

                if (idProperty != null)
                {
                    idProperty.SetValue(record, id);
                }
            }
        }
        private int SafeCreate(string model, XmlRpcStruct values)
        {
            int id = 0;
            int tries = 0;

            while (id == 0 && tries < 5)
            {
                try
                {
                    id = _objectRpc.create(_credentials.DbName, _credentials.UserId,
                                                                            _credentials.DbPassword, model, "create", values);
                }
                catch (WebException ex)
                {
                    Console.WriteLine("Going to sleep to avoid proxy!");
                    tries++;
                    System.Threading.Thread.Sleep(3000);
                }
            }
            return id;
        }
    }
}
