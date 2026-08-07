// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Teams.Exceptions
{
    public class TeamValidationException : Xeption
    {
        public TeamValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
