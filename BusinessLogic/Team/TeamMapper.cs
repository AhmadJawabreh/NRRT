/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.TeamMember;
using Contracts.V1.Team.Models;
using Contracts.V1.Team.Resources;
using Contracts.V1.TeamMember.Resources;
using DataAccess.SQL.Entities;

namespace BusinessLogic.Team
{
    public static class TeamMapper
    {
        public static TeamResource ToResource(this TeamEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                TeamMember =entity.TeamMembers is not null ? entity.TeamMembers.Select(entity => entity.ToResource(false)).ToList(): new List<TeamMemberResource>(),
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                ModifiedBy = entity.ModifiedBy,
                ModifiedOn = entity.ModifiedOn,
            };
        }

        public static TeamEntity ToEntity(this TeamModel model, string userName, int id = 0)
        {
            return new TeamEntity()
            {
                Id = id,
                Name = model.Name,
                Description= model.Description,
            }.WithTracableInformation(userName, id == 0);
        }

        public static TeamEntity WithTracableInformation(this TeamEntity source, string userName, bool isNewEntity = false)
        {
            if (isNewEntity)
            {
                source.CreatedBy = userName;
                source.CreatedOn = DateTimeOffset.UtcNow;
            }

            source.ModifiedBy = userName;
            source.ModifiedOn = DateTimeOffset.UtcNow;

            return source;
        }

        public static void WithCreatedInformation(this TeamEntity destination, TeamEntity source)
        {
            destination.CreatedBy = source.CreatedBy;
            destination.CreatedOn = source.CreatedOn;
        }

        public static void WithDeletedInformation(this TeamEntity destination, string userName)
        {
            destination.IsDeleted = true;
            destination.ModifiedBy = userName;
            destination.ModifiedOn = DateTimeOffset.UtcNow;
        }
    }
}
