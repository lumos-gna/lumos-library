using System.Threading.Tasks;

namespace Lumos.DevKit
{
    public interface IPreInitialize
    {
        public int PreInitOrder { get; }
        public bool PreInitialized { get; }
        public void PreInit();
    }
}