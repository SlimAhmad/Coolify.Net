// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Applications.Exceptions
{
    public class TimeoutApplicationProcessingException : Xeption
    {
        public TimeoutApplicationProcessingException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
