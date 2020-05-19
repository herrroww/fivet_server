using System;
using System.Threading;
using System.Threading.Tasks;
using Fivet.ZeroIce.model;
using Ice;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fivet.Server
{
    internal class FivetService : IHostedService
    {

        ///<summary>
        /// The Logger.
        ///</summary>
        private readonly ILogger<FivetService> _logger;

        ///<summary>
        /// The Port.
        ///</summary>
        private readonly int _port = 8080;

        ///<summary>
        /// The Communicator.
        ///</summary>
        private readonly Communicator _communicator;

        ///<summary>
        /// The FivetService.
        ///</summary>
        public FivetService(ILogger<FivetService> logger)
        {
            _logger = logger;
            _communicator = buildCommunicator();
        }

        private Communicator buildCommunicator()
        {
            _logger.LogDebug("Initializing Communicator v{0} ({1}) ..", Ice.Util.stringVersion(), Ice.Util.intVersion());

            Properties properties = Util.createProperties();

            InitializationData initializationData = new InitializationData();
            initializationData.properties = properties;
            return Ice.Util.initialize(initializationData);

        }
        
        ///<summary>
        /// Triggered when the application host is ready to start the service
        ///</summary>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting the FivetService ... ");

            var adapter = _communicator.createObjectAdapterWithEndpoints("TheAdapter", "tcp -z -t 15000 -p " + _port);

            // The interface
            TheSystem theSystem = new TheSystemImpl();

            // Register in the communicator
            adapter.add(theSystem, Util.stringToIdentity("TheSystem"));

            // Activation
            adapter.activate();

            //All ok
            return Task.CompletedTask;

        }
        ///<summary>
        /// Triggered when the application host is performing a graceful shutdown
        ///</summary>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping the FivetService ... ");

            _communicator.shutdown();

            _logger.LogDebug("Communication Stopped!");

            return Task.CompletedTask;

        }
    }

    ///<summary>
    /// The implementation of TheSystem interface
    ///</summary>
    public class TheSystemImpl : TheSystemDisp_
    {
        public override long getDelay(long clientTime, Current current = null)
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - clientTime;
        }
    }
}