// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Models.Foundations.Servers.Exceptions;

namespace Coolify.Net.Services.Foundations.Servers
{
    public partial class ServerService
    {
        private void ValidateServer(Server server)
        {
            ValidateServerIsNotNull(server);

            Validate(
                message: "Invalid server. Please fix the errors and try again.",

                (Rule: IsInvalid(server.Name), Parameter: nameof(Server.Name)),
                (Rule: IsInvalid(server.Ip), Parameter: nameof(Server.Ip)),
                (Rule: IsInvalid(server.User), Parameter: nameof(Server.User)));
        }

        private void ValidateServerUuid(string serverUuid) =>
            Validate(
                message: "Invalid server. Please fix the errors and try again.",
                (Rule: IsInvalid(serverUuid), Parameter: nameof(serverUuid)));

        private static void ValidateServerIsNotNull(Server server)
        {
            if (server is null)
            {
                throw new NullServerException(message: "Server is null.");
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(string message, params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidServerException = new InvalidServerException(message);

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidServerException.UpsertDataList(key: parameter, value: rule.Message);
                }
            }

            invalidServerException.ThrowIfContainsErrors();
        }
    }
}
