using System.Collections.Generic;

namespace OdooRpcWrapper
{
    public interface IOdooRepository<T> 
        where T : class
    {
        IList<T> ReadAll();

        IList<T> Read(object[] filter);

        void Add(IEnumerable<T> entity);

        void Remove(IEnumerable<T> entity);

        void Update(IEnumerable<T> entity);
        
    }
}
