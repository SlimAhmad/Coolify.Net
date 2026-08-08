// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.CoolifyServices.Exceptions
{
    public class NotFoundCoolifyServiceException : Xeption
    {
        public NotFoundCoolifyServiceException(string message)
            : base(message)
        { }
    }
}
