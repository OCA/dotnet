using System;
using System.Collections.Generic;

namespace OdooRpcWrapper
{
    public interface IOdooAPI
    {
        OdooLoginResult Login();
        void Remove<T>(IEnumerable<T> records);
        int[] Search<T>(object[] filter);
        IList<T> Read<T>(int[] ids);
        void Read<T>(IEnumerable<T> records);
        void Write<T>(IEnumerable<T> records);
        void Create<T>(IEnumerable<T> records);
        bool Execute_Workflow(string model, string action, int id);
        bool ValidateModelType(Type type);
    }
}
