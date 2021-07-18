using System.Collections.Generic;

namespace FlightsSystem.Core.DAL
{
    public interface IBasicDB<T> where T : IPoco
    {
        T Get(long id);
        IList<T> GetAll();
        void Add(T t);
        void Remove(T t);
        void Update(T t);
    }
}