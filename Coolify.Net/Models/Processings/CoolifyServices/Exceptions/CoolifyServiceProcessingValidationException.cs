// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.CoolifyServices.Exceptions
{
    public class CoolifyServiceProcessingValidationException : Xeption
    {
        public CoolifyServiceProcessingValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
