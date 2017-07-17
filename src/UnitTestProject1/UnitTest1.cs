using System;
using GrainInterfaces1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo();
            GrainClient.Initialize(config);
            var friend = GrainClient.GrainFactory.GetGrain<IHello>(0);
            var result = friend.SayHello("Goodbye").Result;
            Assert.IsNotNull(result);
        }
    }
}
