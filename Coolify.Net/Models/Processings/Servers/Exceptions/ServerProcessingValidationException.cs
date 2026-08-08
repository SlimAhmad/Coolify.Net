// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Servers.Exceptions
{
    public class ServerProcessingValidationException : Xeption
    {
        public ServerProcessingValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
