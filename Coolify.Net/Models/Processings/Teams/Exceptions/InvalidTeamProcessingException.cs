// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Teams.Exceptions
{
    public class InvalidTeamProcessingException : Xeption
    {
        public InvalidTeamProcessingException(string message)
            : base(message)
        { }
    }
}
