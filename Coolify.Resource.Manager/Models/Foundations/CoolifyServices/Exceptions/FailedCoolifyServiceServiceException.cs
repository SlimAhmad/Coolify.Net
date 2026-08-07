// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.CoolifyServices.Exceptions
{
    public class FailedCoolifyServiceServiceException : Xeption
    {
        public FailedCoolifyServiceServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
