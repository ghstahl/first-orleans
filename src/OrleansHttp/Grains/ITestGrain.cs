using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace OrleansHttp.Grains
{
    public interface ITestGrain : IGrainWithStringKey
    {
        Task Test();
    }
}
