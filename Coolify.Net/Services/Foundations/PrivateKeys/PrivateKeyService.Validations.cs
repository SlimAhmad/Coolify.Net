// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.PrivateKeys;
using Coolify.Net.Models.Foundations.PrivateKeys.Exceptions;

namespace Coolify.Net.Services.Foundations.PrivateKeys
{
    public partial class PrivateKeyService
    {
        private void ValidatePrivateKey(PrivateKey privateKey)
        {
            ValidatePrivateKeyIsNotNull(privateKey);

            Validate(
                (IsInvalid(privateKey.Name), nameof(PrivateKey.Name)),
                (IsInvalid(privateKey.PrivateKeyValue), nameof(PrivateKey.PrivateKeyValue)));
        }

        private void ValidatePrivateKeyUuid(string privateKeyUuid) =>
            Validate((IsInvalid(privateKeyUuid), nameof(privateKeyUuid)));

        private static void ValidatePrivateKeyIsNotNull(PrivateKey privateKey)
        {
            if (privateKey is null)
            {
                throw new NullPrivateKeyException(message: "Private key is null.");
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidPrivateKeyException =
                new InvalidPrivateKeyException(
                    message: "Invalid private key. Please fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidPrivateKeyException.UpsertDataList(key: parameter, value: rule.Message);
                }
            }

            invalidPrivateKeyException.ThrowIfContainsErrors();
        }
    }
}
