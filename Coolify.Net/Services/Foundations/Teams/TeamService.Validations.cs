// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Teams.Exceptions;

namespace Coolify.Net.Services.Foundations.Teams
{
    public partial class TeamService
    {
        private void ValidateTeamId(int id) =>
            Validate((IsInvalid(id), nameof(id)));

        private static dynamic IsInvalid(int id) => new
        {
            Condition = id <= 0,
            Message = "Id is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidTeamException =
                new InvalidTeamException(
                    message: "Invalid team. Please fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidTeamException.UpsertDataList(key: parameter, value: rule.Message);
                }
            }

            invalidTeamException.ThrowIfContainsErrors();
        }
    }
}
