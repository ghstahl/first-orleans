using System.Threading.Tasks;
using Orleans;

namespace GrainInterfaces1
{
    /// <summary>
    /// Grain interface IGrain1
    /// </summary>
    public interface IHello : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }
}
