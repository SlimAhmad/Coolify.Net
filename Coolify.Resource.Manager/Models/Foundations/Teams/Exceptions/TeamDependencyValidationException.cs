// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Teams.Exceptions
{
    public class TeamDependencyValidationException : Xeption
    {
        public TeamDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
