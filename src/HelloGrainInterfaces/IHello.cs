using System.Threading.Tasks;
using Orleans;

namespace HelloGrainInterfaces
{
    /// <summary>
    /// Grain interface IHello
    /// </summary>
    public interface IHello : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }
}
