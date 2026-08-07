// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.CoolifyServices.Exceptions
{
    public class TimeoutCoolifyServiceProcessingException : Xeption
    {
        public TimeoutCoolifyServiceProcessingException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
