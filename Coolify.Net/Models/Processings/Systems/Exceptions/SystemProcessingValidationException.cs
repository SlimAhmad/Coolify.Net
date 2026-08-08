// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Systems.Exceptions
{
    public class SystemProcessingValidationException : Xeption
    {
        public SystemProcessingValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
