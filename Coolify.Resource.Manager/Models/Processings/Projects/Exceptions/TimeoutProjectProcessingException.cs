// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Projects.Exceptions
{
    public class TimeoutProjectProcessingException : Xeption
    {
        public TimeoutProjectProcessingException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
