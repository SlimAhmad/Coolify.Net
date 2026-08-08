// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.CoolifyServices.Exceptions
{
    public class CoolifyServiceProcessingServiceException : Xeption
    {
        public CoolifyServiceProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
