// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Projects.Exceptions
{
    public class TimeoutProjectProcessingException : Xeption
    {
        public TimeoutProjectProcessingException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
