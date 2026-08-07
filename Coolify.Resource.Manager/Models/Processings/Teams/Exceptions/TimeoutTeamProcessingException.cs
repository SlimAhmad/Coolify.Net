// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Teams.Exceptions
{
    public class TimeoutTeamProcessingException : Xeption
    {
        public TimeoutTeamProcessingException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
