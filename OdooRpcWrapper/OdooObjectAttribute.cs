using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OdooRpcWrapper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OdooObjectAttribute : Attribute
    {
        private readonly string _name;
        public OdooObjectAttribute(string name)
        {
            _name = name;
        }

        public string Name { get { return _name; } }
        
        #region Static Help Functions
        public static bool IsOdooObject(Object obj)
        {
            Type objectType = obj.GetType();

            return IsOdooObject(objectType);
        }
        public static bool IsOdooObject(Type objectType)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(objectType);

            foreach (Attribute attr in attrs)
            {
                if (attr is OdooObjectAttribute)
                {
                    return true;
                }
            }

            return false;
        }
        public static string GetModelName(Object obj)
        {
            Type objectType = obj.GetType();

            return GetModelName(objectType);
        }
        public static string GetModelName(Type objectType)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(objectType);

            foreach (Attribute attr in attrs)
            {
                if (attr is OdooObjectAttribute)
                {
                    OdooObjectAttribute odooAttribute = (OdooObjectAttribute)attr;
                    return odooAttribute.Name;
                }
            }

            return null;
        }
        #endregion
    }
}
