// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Projects.Exceptions
{
    public class NullProjectProcessingException : Xeption
    {
        public NullProjectProcessingException(string message)
            : base(message)
        { }
    }
}
