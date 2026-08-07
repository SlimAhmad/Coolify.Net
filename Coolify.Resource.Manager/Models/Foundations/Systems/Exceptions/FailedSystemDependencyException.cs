// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Systems.Exceptions
{
    public class FailedSystemDependencyException : Xeption
    {
        public FailedSystemDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
