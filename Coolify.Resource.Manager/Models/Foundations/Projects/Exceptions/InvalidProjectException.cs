// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Projects.Exceptions
{
    public class InvalidProjectException : Xeption
    {
        public InvalidProjectException(string message)
            : base(message)
        { }

        public InvalidProjectException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
