using System.Collections.Generic;

namespace LumosLib
{
    public interface IDataManager
    {
        public List<T> GetDataAll<T>() where T : IData;
    }
}