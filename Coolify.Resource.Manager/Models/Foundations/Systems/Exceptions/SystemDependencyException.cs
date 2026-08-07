// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Systems.Exceptions
{
    public class SystemDependencyException : Xeption
    {
        public SystemDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
