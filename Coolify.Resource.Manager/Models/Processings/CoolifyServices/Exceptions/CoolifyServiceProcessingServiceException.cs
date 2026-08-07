// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.CoolifyServices.Exceptions
{
    public class CoolifyServiceProcessingServiceException : Xeption
    {
        public CoolifyServiceProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
