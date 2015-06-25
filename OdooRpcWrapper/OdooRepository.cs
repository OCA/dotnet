using System.Collections.Generic;

namespace OdooRpcWrapper
{
    public class OdooRepository<T> : IOdooRepository<T>
        where T : class
    {
        private readonly IOdooAPI _api;

        public OdooRepository(IOdooAPI api)
        {
            _api = api;
        }

        public IList<T> ReadAll()
        {
            object[] filter = new object[0];
            return Read(filter);
        }

        public IList<T> Read(object[] filter)
        {
            int[] ids = _api.Search<T>(filter);
            if (ids == null)
            {
                return new List<T>();
            }
            else
            {
                return _api.Read<T>(ids);
            }
        }

        public void Add(IEnumerable<T> entities)
        {
            _api.Create(entities);
        }

        public void Remove(IEnumerable<T> entities)
        {
            _api.Remove(entities);
        }

        public void Update(IEnumerable<T> entities)
        {
            _api.Write(entities);
        }
    }
}
