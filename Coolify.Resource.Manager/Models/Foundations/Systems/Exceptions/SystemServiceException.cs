// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Systems.Exceptions
{
    public class SystemServiceException : Xeption
    {
        public SystemServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
