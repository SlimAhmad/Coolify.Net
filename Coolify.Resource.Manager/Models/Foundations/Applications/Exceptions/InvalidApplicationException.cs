// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Applications.Exceptions
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
