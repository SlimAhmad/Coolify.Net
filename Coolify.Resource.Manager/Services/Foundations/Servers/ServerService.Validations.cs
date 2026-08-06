// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers.Exceptions;

namespace Coolify.Resource.Manager.Services.Foundations.Servers
{
    public partial class ServerService
    {
        private void ValidateServer(Server server)
        {
            ValidateServerIsNotNull(server);

            Validate(
                (IsInvalid(server.Name), nameof(Server.Name)),
                (IsInvalid(server.Ip), nameof(Server.Ip)),
                (IsInvalid(server.User), nameof(Server.User)));
        }

        private void ValidateServerUuid(string serverUuid) =>
            Validate((IsInvalid(serverUuid), nameof(serverUuid)));

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

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidServerException =
                new InvalidServerException(
                    message: "Invalid server. Please fix the errors and try again.");

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
