using System.Collections;

namespace LumosLib
{
    public abstract class BaseDataLoader
    {
        protected string _path;

        public string Json { get; protected set; }


        public void SetPath(string path)
        {
            _path = path;
        }

        public abstract IEnumerator LoadJsonAsync();
    }
}