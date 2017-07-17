using System.Threading.Tasks;
using GrainInterfaces1;
using Orleans;

namespace Grains1
{
    /// <summary>
    /// Grain implementation class Grain1.
    /// </summary>
    public class HelloGrain : Grain, IHello
    {
        public Task<string> SayHello(string greeting)
        {
            return Task.FromResult("You said: '" + greeting + "', I say: Hello!");
        }
    }
}
