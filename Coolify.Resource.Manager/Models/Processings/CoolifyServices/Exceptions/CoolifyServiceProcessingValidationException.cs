// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.CoolifyServices.Exceptions
{
    public class CoolifyServiceProcessingValidationException : Xeption
    {
        public CoolifyServiceProcessingValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
