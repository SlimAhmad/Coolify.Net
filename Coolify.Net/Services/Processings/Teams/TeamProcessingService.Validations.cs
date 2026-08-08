// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Processings.Teams.Exceptions;

namespace Coolify.Net.Services.Processings.Teams
{
    public partial class TeamProcessingService
    {
        private static void ValidateTeamId(int id)
        {
            if (id <= 0)
            {
                throw new InvalidTeamProcessingException(message: "Team id is invalid.");
            }
        }
    }
}
