// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Teams.Exceptions
{
    public class TimeoutTeamException : Xeption
    {
        public TimeoutTeamException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
