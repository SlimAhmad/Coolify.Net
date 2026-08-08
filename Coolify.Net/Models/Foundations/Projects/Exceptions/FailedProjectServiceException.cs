// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Projects.Exceptions
{
    public class FailedProjectServiceException : Xeption
    {
        public FailedProjectServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
