// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Systems.Exceptions
{
    public class FailedSystemDependencyException : Xeption
    {
        public FailedSystemDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
