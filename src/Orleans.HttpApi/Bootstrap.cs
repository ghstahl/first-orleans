using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Orleans.Providers;
using Orleans.Runtime;

namespace Orleans.HttpApi
{
    public class Bootstrap : IBootstrapProvider
    {
        IDisposable host;
        private int initCount;
        public Bootstrap()
        {
#if DEBUG
            // Note: Can't use logger here because it is not initialized until the Init method is called.
            Console.WriteLine("Constructor - Orleans.HttpApi Bootstrap");
#endif
        }
        public Task Init(string name, IProviderRuntime providerRuntime, IProviderConfiguration config)
        {
            Name = name;
            logger = providerRuntime.GetLogger(GetType().Name);
            logger.Info("Init Name={0}", Name);
            Interlocked.Increment(ref initCount);

            var router = new Router();
            new GrainController(router, TaskScheduler.Current, providerRuntime);
            var options = new StartOptions
            {
                ServerFactory = "Nowin",
                Port = config.Properties.ContainsKey("Port") ? int.Parse(config.Properties["Port"]) : 8080,
            };

            var username = config.Properties.ContainsKey("Username") ? config.Properties["Username"] : null;
            var password = config.Properties.ContainsKey("Password") ? config.Properties["Password"] : null;

            host = WebApp.Start(options, app => new WebServer(router, username, password).Configure(app));
            return Task.CompletedTask;
        }

        public Task Close()
        {
            logger.Info("Close Name={0}", Name);
            return Task.CompletedTask;
        }

        public string Name { get; private set; }
        protected Logger logger { get; private set; }
    }
}
