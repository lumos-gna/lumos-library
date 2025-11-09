using System.Collections.Generic;

namespace LumosLib
{
    public interface IDataManager : IGlobal
    {
        public List<T> GetDataAll<T>() where T : IData;
    }
}