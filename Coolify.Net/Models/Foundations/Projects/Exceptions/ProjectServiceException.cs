// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Projects.Exceptions
{
    public class ProjectServiceException : Xeption
    {
        public ProjectServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
