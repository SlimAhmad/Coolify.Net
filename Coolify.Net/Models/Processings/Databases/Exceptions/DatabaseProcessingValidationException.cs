// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Databases.Exceptions
{
    public class DatabaseProcessingValidationException : Xeption
    {
        public DatabaseProcessingValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
