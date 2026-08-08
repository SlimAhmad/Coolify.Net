// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Applications.Exceptions
{
    public class ApplicationServiceException : Xeption
    {
        public ApplicationServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
