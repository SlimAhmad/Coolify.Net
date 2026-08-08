// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.CoolifyServices.Exceptions
{
    public class NullCoolifyServiceProcessingException : Xeption
    {
        public NullCoolifyServiceProcessingException(string message)
            : base(message)
        { }
    }
}
