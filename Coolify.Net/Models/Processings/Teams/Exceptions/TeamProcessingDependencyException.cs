// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Teams.Exceptions
{
    public class TeamProcessingDependencyException : Xeption
    {
        public TeamProcessingDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
