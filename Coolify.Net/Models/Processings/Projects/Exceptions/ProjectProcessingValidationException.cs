// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Projects.Exceptions
{
    public class ProjectProcessingValidationException : Xeption
    {
        public ProjectProcessingValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
