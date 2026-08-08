// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Teams.Exceptions
{
    public class TimeoutTeamProcessingException : Xeption
    {
        public TimeoutTeamProcessingException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
