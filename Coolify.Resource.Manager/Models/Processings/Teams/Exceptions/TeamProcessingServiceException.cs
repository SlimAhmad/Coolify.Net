// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Teams.Exceptions
{
    public class TeamProcessingServiceException : Xeption
    {
        public TeamProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
