// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.CoolifyServices.Exceptions
{
    public class CoolifyServiceServiceException : Xeption
    {
        public CoolifyServiceServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
