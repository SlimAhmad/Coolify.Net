// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.CoolifyServices.Exceptions
{
    public class CoolifyServiceDependencyValidationException : Xeption
    {
        public CoolifyServiceDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
