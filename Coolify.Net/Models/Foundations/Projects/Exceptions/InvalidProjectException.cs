// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Projects.Exceptions
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
