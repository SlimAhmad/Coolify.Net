// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Tests.Acceptance.Brokers;

namespace Coolify.Net.Tests.Acceptance.Systems
{
    [Collection(nameof(ClientTestCollection))]
    public partial class SystemClientTests
    {
        private readonly ClientBroker clientBroker;

        public SystemClientTests(ClientBroker clientBroker)
        {
            this.clientBroker = clientBroker;
            this.clientBroker.Reset();
        }
    }
}
