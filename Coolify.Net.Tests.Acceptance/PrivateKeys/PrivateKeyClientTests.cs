// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Tests.Acceptance.Brokers;

namespace Coolify.Net.Tests.Acceptance.PrivateKeys
{
    [Collection(nameof(ClientTestCollection))]
    public partial class PrivateKeyClientTests
    {
        private readonly ClientBroker clientBroker;

        public PrivateKeyClientTests(ClientBroker clientBroker)
        {
            this.clientBroker = clientBroker;
            this.clientBroker.Reset();
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();
    }
}
