using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloGrainInterfaces;
using Orleans;
using Orleans.Runtime.Host;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = AzureClient.DefaultConfiguration();
            AzureClient.Initialize(config);
            //GrainClient.Initialize(config);
            var friend = GrainClient.GrainFactory.GetGrain<IHello>(0);
            var result = friend.SayHello("Goodbye").Result;
            Console.WriteLine(result);
        }
    }
}
