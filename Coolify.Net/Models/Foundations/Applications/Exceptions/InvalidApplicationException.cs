// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Applications.Exceptions
{
    public class InvalidApplicationException : Xeption
    {
        public InvalidApplicationException(string message)
            : base(message)
        { }

        public InvalidApplicationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
