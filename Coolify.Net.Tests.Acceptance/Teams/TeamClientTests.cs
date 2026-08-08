// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Tests.Acceptance.Brokers;

namespace Coolify.Net.Tests.Acceptance.Teams
{
    [Collection(nameof(ClientTestCollection))]
    public partial class TeamClientTests
    {
        private readonly ClientBroker clientBroker;

        public TeamClientTests(ClientBroker clientBroker)
        {
            this.clientBroker = clientBroker;
            this.clientBroker.Reset();
        }

        private static int GetRandomId() => new Random().Next(1, 1000);
    }
}
