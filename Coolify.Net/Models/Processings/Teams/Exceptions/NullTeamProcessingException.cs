// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Teams.Exceptions
{
    public class NullTeamProcessingException : Xeption
    {
        public NullTeamProcessingException(string message)
            : base(message)
        { }
    }
}
