// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.CoolifyServices.Exceptions
{
    public class CoolifyServiceProcessingDependencyException : Xeption
    {
        public CoolifyServiceProcessingDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
