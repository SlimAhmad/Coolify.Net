// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Tests.Acceptance.Brokers;

namespace Coolify.Net.Tests.Acceptance.Databases
{
    [Collection(nameof(ClientTestCollection))]
    public partial class DatabaseClientTests
    {
        private readonly ClientBroker clientBroker;

        public DatabaseClientTests(ClientBroker clientBroker)
        {
            this.clientBroker = clientBroker;
            this.clientBroker.Reset();
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();
    }
}
