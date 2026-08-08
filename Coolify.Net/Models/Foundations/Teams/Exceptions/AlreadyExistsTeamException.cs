// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Teams.Exceptions
{
    public class AlreadyExistsTeamException : Xeption
    {
        public AlreadyExistsTeamException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
