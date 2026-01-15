using System.Collections.Generic;
using UnityEngine;

namespace LumosLib
{
    public interface IResourceManager
    {
        public T Get<T>(string assetName);
        public T Get<T>(string label, string key);
        public List<T> GetAll<T>(string label);
    }
}