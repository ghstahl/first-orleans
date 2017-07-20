using System;

using HelloGrainInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans;
using Orleans.Runtime.Host;

namespace UnitTestProject1
{
    [TestClass]
    public class FirstOrleansTest
    {
        [TestMethod]
        public void TestMethod_AzureWebRole()
        {
            var config = AzureClient.DefaultConfiguration();
            AzureClient.Initialize(config);
            //GrainClient.Initialize(config);
            var friend = GrainClient.GrainFactory.GetGrain<IHello>(0);
            var result = friend.SayHello("Goodbye").Result;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TestMethod_LocalSiloHost()
        {
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo();
            GrainClient.Initialize(config);
            var friend = GrainClient.GrainFactory.GetGrain<IHello>(0);
            var result = friend.SayHello("Goodbye").Result;
            Console.WriteLine(result);
            Assert.IsNotNull(result);
        }
    }
}
