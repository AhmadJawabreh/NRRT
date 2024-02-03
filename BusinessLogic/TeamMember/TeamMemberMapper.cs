/*
 * Copyright(C) 2024 - present NRRT.
 * All rights reserved.
 */

using BusinessLogic.Team;
using Contracts.V1.TeamMember.Models;
using Contracts.V1.TeamMember.Resources;
using DataAccess.SQL.Entities;

namespace BusinessLogic.TeamMember
{
    public static class TeamMemberMapper
    {
        public static TeamMemberResource ToResource(this TeamMemberEntity entity, bool withTeams = true)
        {
            return new()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                FatherName = entity.FatherName,
                GrandFatherName = entity.GrandFatherName,
                LastName = entity.LastName,
                Position = entity.Position,
                PhoneNumber = entity.PhoneNumber,
                Description = entity.Description,
                Team = withTeams ? entity.TeamEntity!.ToResource(): null,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                ModifiedBy = entity.ModifiedBy,
                ModifiedOn = entity.ModifiedOn,
            };
        }

        public static TeamMemberEntity ToEntity(this TeamMemberModel model, string userName, int id = 0)
        {
            return new TeamMemberEntity()
            {
                Id = id,
                FirstName = model.FirstName,
                FatherName = model.FatherName,
                GrandFatherName = model.GrandFatherName,
                LastName = model.LastName,
                TeamId = model.TeamId,
                Position = model.Position,
                PhoneNumber = model.PhoneNumber,
                Description = model.Description,
            }.WithTracableInformation(userName, id == 0);
        }

        public static TeamMemberEntity WithTracableInformation(this TeamMemberEntity source, string userName, bool isNewEntity = false)
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

        public static void WithCreatedInformation(this TeamMemberEntity destination, TeamMemberEntity source)
        {
            destination.CreatedBy = source.CreatedBy;
            destination.CreatedOn = source.CreatedOn;
        }

        public static void WithDeletedInformation(this TeamMemberEntity destination, string userName)
        {
            destination.IsDeleted = true;
            destination.ModifiedBy = userName;
            destination.ModifiedOn = DateTimeOffset.UtcNow;
        }
    }
}
