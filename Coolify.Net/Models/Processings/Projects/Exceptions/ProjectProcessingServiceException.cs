// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Projects.Exceptions
{
    public class ProjectProcessingServiceException : Xeption
    {
        public ProjectProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
