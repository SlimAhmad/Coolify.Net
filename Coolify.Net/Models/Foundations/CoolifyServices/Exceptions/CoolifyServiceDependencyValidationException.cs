// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.CoolifyServices.Exceptions
{
    public class CoolifyServiceDependencyValidationException : Xeption
    {
        public CoolifyServiceDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
