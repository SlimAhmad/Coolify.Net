// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.CoolifyServices.Exceptions
{
    public class FailedCoolifyServiceDependencyException : Xeption
    {
        public FailedCoolifyServiceDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
