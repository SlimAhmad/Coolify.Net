// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.CoolifyServices.Exceptions
{
    public class CoolifyServiceDependencyException : Xeption
    {
        public CoolifyServiceDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
