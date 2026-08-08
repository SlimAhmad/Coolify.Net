// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Systems.Exceptions
{
    public class NullSystemProcessingException : Xeption
    {
        public NullSystemProcessingException(string message)
            : base(message)
        { }
    }
}
