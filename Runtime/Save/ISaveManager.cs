using Cysharp.Threading.Tasks;

namespace LumosLib
{
    public interface ISaveManager
    {
        public UniTask SaveAsync<T>(T data);
        public UniTask<T> LoadAsync<T>();
    }
}