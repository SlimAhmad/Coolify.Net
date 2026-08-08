// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.CoolifyServices.Exceptions
{
    public class CoolifyServiceProcessingDependencyException : Xeption
    {
        public CoolifyServiceProcessingDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
