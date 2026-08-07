// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Systems.Exceptions
{
    public class SystemProcessingDependencyException : Xeption
    {
        public SystemProcessingDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
