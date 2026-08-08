// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Servers.Exceptions
{
    public class InvalidServerException : Xeption
    {
        public InvalidServerException(string message)
            : base(message)
        { }

        public InvalidServerException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
