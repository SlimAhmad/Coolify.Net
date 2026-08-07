// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Teams.Exceptions
{
    public class NullTeamProcessingException : Xeption
    {
        public NullTeamProcessingException(string message)
            : base(message)
        { }
    }
}
