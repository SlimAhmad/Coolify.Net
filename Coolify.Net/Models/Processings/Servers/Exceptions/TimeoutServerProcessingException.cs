// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Servers.Exceptions
{
    public class TimeoutServerProcessingException : Xeption
    {
        public TimeoutServerProcessingException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
