namespace LumosLib
{
    public interface IPoolable
    {
        public void OnGet();
        public void OnRelease();
    }
}

