// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Databases.Exceptions
{
    public class NullDatabaseProcessingException : Xeption
    {
        public NullDatabaseProcessingException(string message)
            : base(message)
        { }
    }
}
