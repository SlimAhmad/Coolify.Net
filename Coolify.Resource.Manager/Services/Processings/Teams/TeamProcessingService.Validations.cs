// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Processings.Teams.Exceptions;

namespace Coolify.Resource.Manager.Services.Processings.Teams
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
