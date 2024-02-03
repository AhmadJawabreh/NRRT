/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1.Team.Filters;
using Contracts.V1.Team.Models;
using Contracts.V1.Team.Resources;
using DataAccess.SQL.Entities;
using DataAccess.SQL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Shared.Constants;
using Shared.GlobalExceptionHandler.Exceptions;
using System.Linq.Expressions;
using System.Security.Claims;

namespace BusinessLogic.Team
{
    public interface ITeamManager
    {
        public Task<List<TeamResource>> GetItems(TeamFilter filter);

        public Task<TeamResource> GetItemById(int id);

        public Task<TeamResource> CreateAsync(TeamModel model);

        public Task<TeamResource> UpdateAsync(int id, TeamModel model);

        public Task DeleteAsync(int id);

        public Task<int> CountAsync();
    }

    public class TeamManager : ITeamManager
    {
        private readonly string _userName;

        private readonly IUnitOfWork _unitOfWork;

        private readonly List<Expression<Func<TeamEntity, object>>> _includes = new() { x => x.TeamMembers! };

        public TeamManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        }

        public async Task<List<TeamResource>> GetItems(TeamFilter filter)
        {
            return (await _unitOfWork
                .TeamRepository
                .GetIteamsAsync(GetExpressions(filter), _includes, filter))
                .Select(item => item.ToResource()).ToList();
        }

        public async Task<TeamResource> GetItemById(int id)
        {
            return (await GetTeamById(id)).ToResource();
        }

        public async  Task<TeamResource> CreateAsync(TeamModel model)
        {
            var isPatientIDExist = await _unitOfWork.TeamRepository.FirstOrDefaultAsync(x => x.Name == model.Name);

            if (isPatientIDExist != null)
            {
                throw new ConflictException(nameof(TeamEntity.Id), ValidationMessages.PatientIsExist(model.Name));
            }

            var entity = model.ToEntity(_userName);

            _unitOfWork.TeamRepository.Create(entity);

            await _unitOfWork.SaveChanges();

            return (await GetTeamById(entity.Id))!.ToResource();
        }


        public async Task<TeamResource> UpdateAsync(int id, TeamModel model)
        {
            var isTeamNameExist = await _unitOfWork.TeamRepository.FirstOrDefaultAsync(x => x.Name == model.Name && x.Id != id);

            if (isTeamNameExist != null)
            {
                throw new ConflictException(nameof(TeamEntity.Id), ValidationMessages.PatientIsExist(model.Name));
            }

            var entity = await GetTeamById(id);

            var updatedTeam = model.ToEntity(_userName, id);

            updatedTeam.WithCreatedInformation(entity);

            _unitOfWork.TeamRepository.Update(updatedTeam);

            await _unitOfWork.SaveChanges();

            return (await GetTeamById(entity.Id))!.ToResource();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetTeamById(id);


            entity.WithDeletedInformation(_userName);

            _unitOfWork.TeamRepository.Delete(entity);

            await _unitOfWork.SaveChanges();
        }

        public async Task<int> CountAsync()
        {
            return await _unitOfWork.TeamRepository.CountAsync(x => !x.IsDeleted);
        }

        private async Task<TeamEntity> GetTeamById(int id)
        {
           var entity =  await _unitOfWork.TeamRepository.FirstOrDefaultAsync(x => x.Id == id, _includes);

            if (entity == null)
            {
                throw new ItemNotFoundException(nameof(id), "the team does not exist");
            }
            return entity;
        }

        private static List<Expression<Func<TeamEntity, bool>>> GetExpressions(TeamFilter filter)
        {
            List<Expression<Func<TeamEntity, bool>>> experssions = new();

            if (filter.Id is not 0)
            {
                experssions.Add(item => item.Id == filter.Id);
            }

            if (filter.Name is not null)
            {
                experssions.Add(item => item.Name == filter.Name);
            }
            return experssions;
        }
    }
}
