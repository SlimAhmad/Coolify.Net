// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Projects.Exceptions
{
    public class ProjectProcessingServiceException : Xeption
    {
        public ProjectProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
