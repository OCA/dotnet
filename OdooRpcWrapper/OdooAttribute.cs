using System;
using System.Reflection;

namespace OdooRpcWrapper
{
    public abstract class OdooAttribute : Attribute
    {
        private readonly string _name;

        public String Name { get { return _name; } }

        public OdooAttribute(string name)
        {
            _name = name;
        }

        #region Static Help Functions
        public static bool HasOdooAttribute(PropertyInfo property)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(property);

            foreach (Attribute attr in attrs)
            {
                if (attr is OdooAttribute)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool HasOdooPropertyAttribute(PropertyInfo property)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(property);

            foreach (Attribute attr in attrs)
            {
                if (attr is OdooPropertyAttribute)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool HasOdooOne2manyAttribute(PropertyInfo property)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(property);

            foreach (Attribute attr in attrs)
            {
                if (attr is OdooOne2manyAttribute)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool HasOdooMany2oneAttribute(PropertyInfo property)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(property);

            foreach (Attribute attr in attrs)
            {
                if (attr is OdooMany2oneAttribute)
                {
                    return true;
                }
            }

            return false;
        }
        public static string GetFieldName(PropertyInfo property)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(property);

            foreach (Attribute attr in attrs)
            {
                if (attr is OdooAttribute)
                {
                    OdooAttribute odooAttribute = (OdooAttribute)attr;
                    return odooAttribute.Name;
                }
            }

            return null;
        }
        #endregion
    }
}
