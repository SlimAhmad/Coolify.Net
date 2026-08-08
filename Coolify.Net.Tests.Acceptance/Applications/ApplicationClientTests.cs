// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Tests.Acceptance.Brokers;

namespace Coolify.Net.Tests.Acceptance.Applications
{
    [Collection(nameof(ClientTestCollection))]
    public partial class ApplicationClientTests
    {
        private readonly ClientBroker clientBroker;

        public ApplicationClientTests(ClientBroker clientBroker)
        {
            this.clientBroker = clientBroker;
            this.clientBroker.Reset();
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();
    }
}
