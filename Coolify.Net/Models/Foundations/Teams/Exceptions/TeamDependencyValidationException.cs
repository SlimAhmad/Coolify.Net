// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Teams.Exceptions
{
    public class TeamDependencyValidationException : Xeption
    {
        public TeamDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
