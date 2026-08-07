// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Applications.Exceptions
{
    public class ApplicationProcessingServiceException : Xeption
    {
        public ApplicationProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
