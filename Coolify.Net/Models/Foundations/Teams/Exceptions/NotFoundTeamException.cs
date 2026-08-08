// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Teams.Exceptions
{
    public class NotFoundTeamException : Xeption
    {
        public NotFoundTeamException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
