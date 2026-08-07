// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Teams.Exceptions
{
    public class InvalidTeamProcessingException : Xeption
    {
        public InvalidTeamProcessingException(string message)
            : base(message)
        { }
    }
}
