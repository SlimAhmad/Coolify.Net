// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Clients.Coolify.Net;
using WireMock.Server;

namespace Coolify.Net.Tests.Acceptance.Brokers
{
    /// <summary>
    /// Emulates the Coolify REST API (a resource this library does not own) using an
    /// in-process WireMock server, and exposes a real <see cref="CoolifyClient"/> wired to
    /// point at it. Acceptance tests drive the full Broker/Foundation/Processing/Client
    /// stack through this broker's <see cref="Client"/> and assert against the emulated
    /// server's received requests/configured stubs. Shared once per test assembly via
    /// <see cref="ClientTestCollection"/>.
    /// </summary>
    public class ClientBroker : IDisposable
    {
        public const string ApiToken = "acceptance-test-token";

        public WireMockServer Server { get; }
        public ICoolifyClient Client { get; }

        public ClientBroker()
        {
            this.Server = WireMockServer.Start();

            this.Client = new CoolifyClient(options =>
            {
                options.BaseUrl = this.Server.Urls[0];
                options.ApiToken = ApiToken;
            });
        }

        /// <summary>Clears all stubbed mappings and received request logs between tests.</summary>
        public void Reset() =>
            this.Server.Reset();

        public void Dispose() =>
            this.Server.Stop();
    }
}
