using System.Collections.Generic;

namespace LumosLib
{
    public interface IDataTableManager
    {
        public List<T> GetAll<T>() where T : BaseData;
        public T Get<T>(int id) where T : BaseData;
    }
}