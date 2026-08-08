// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Teams.Exceptions
{
    public class InvalidTeamException : Xeption
    {
        public InvalidTeamException(string message)
            : base(message)
        { }

        public InvalidTeamException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
