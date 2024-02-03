/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1.TeamMember.Filters;
using Contracts.V1.TeamMember.Models;
using Contracts.V1.TeamMember.Resources;
using DataAccess.SQL.Entities;
using DataAccess.SQL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Shared.Constants;
using Shared.GlobalExceptionHandler.Exceptions;
using System.Linq.Expressions;
using System.Security.Claims;

namespace BusinessLogic.TeamMember
{
    public interface ITeamMemberManager
    {
        public Task<List<TeamMemberResource>> GetItems(TeamMemberFilter filter);

        public Task<TeamMemberResource> GetItemById(int id);

        public Task<TeamMemberResource> CreateAsync(TeamMemberModel model);

        public Task<TeamMemberResource> UpdateAsync(int id, TeamMemberModel model);

        public Task DeleteAsync(int id);

        public Task<int> CountAsync();
    }

    public class TeamMemberManager : ITeamMemberManager
    {
        private readonly string _userName;

        private readonly IUnitOfWork _unitOfWork;

        private readonly List<Expression<Func<TeamMemberEntity, object>>> _includes = new() { x => x.TeamEntity! };


        public TeamMemberManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        }

        public async Task<List<TeamMemberResource>> GetItems(TeamMemberFilter filter)
        {
            return (await _unitOfWork
                .TeamMemberRepository
                .GetIteamsAsync(GetExpressions(filter), _includes, filter))
                .Select(item => item.ToResource()).ToList();
        }

        public async Task<TeamMemberResource> GetItemById(int id)
        {
            return (await GetTeamMemberById(id)).ToResource();
        }

        public async Task<TeamMemberResource> CreateAsync(TeamMemberModel model)
        {
            var isPatientPhoneNumberExist = await _unitOfWork.TeamMemberRepository.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber);

            if (isPatientPhoneNumberExist != null)
            {
                throw new ConflictException(nameof(TeamMemberEntity.Id), ValidationMessages.PhoneNumberIsExist(model.PhoneNumber));
            }

            var entity = model.ToEntity(_userName);

            _unitOfWork.TeamMemberRepository.Create(entity);

            await _unitOfWork.SaveChanges();

            return (await GetTeamMemberById(entity.Id))!.ToResource();
        }


        public async Task<TeamMemberResource> UpdateAsync(int id, TeamMemberModel model)
        {
            var isTeamMemberPhoneExist = await _unitOfWork.TeamMemberRepository.FirstOrDefaultAsync(x => x.PhoneNumber == model.PhoneNumber && x.Id != id);

            if (isTeamMemberPhoneExist != null)
            {
                throw new ConflictException(nameof(TeamMemberEntity.Id), ValidationMessages.PhoneNumberIsExist(model.PhoneNumber));
            }

            var entity = await GetTeamMemberById(id);

            var updatedTeamMember = model.ToEntity(_userName, id);

            updatedTeamMember.WithCreatedInformation(entity);

            _unitOfWork.TeamMemberRepository.Update(updatedTeamMember);

            await _unitOfWork.SaveChanges();

            return (await GetTeamMemberById(entity.Id))!.ToResource();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetTeamMemberById(id);


            entity.WithDeletedInformation(_userName);

            _unitOfWork.TeamMemberRepository.Delete(entity);

            await _unitOfWork.SaveChanges();
        }

        public async Task<int> CountAsync()
        {
            return await _unitOfWork.TeamMemberRepository.CountAsync(x => !x.IsDeleted);
        }

        private async Task<TeamMemberEntity> GetTeamMemberById(int id)
        {
            var entity = await _unitOfWork.TeamMemberRepository.FirstOrDefaultAsync(x => x.Id == id, _includes);

            if (entity == null)
            {
                throw new ItemNotFoundException(nameof(id), "the Team Member does not exist");
            }
            return entity;
        }

        private static List<Expression<Func<TeamMemberEntity, bool>>> GetExpressions(TeamMemberFilter filter)
        {
            List<Expression<Func<TeamMemberEntity, bool>>> experssions = new();

            if (filter.Id is not 0)
            {
                experssions.Add(item => item.Id == filter.Id);
            }

            if (filter.FirstName is not null)
            {
                experssions.Add(item => item.FirstName == filter.FirstName);
            }

            if (filter.FatherName is not null)
            {
                experssions.Add(item => item.FatherName == filter.FatherName);
            }

            if (filter.GrandFatherName is not null)
            {
                experssions.Add(item => item.GrandFatherName == filter.GrandFatherName);
            }

            if (filter.LastName is not null)
            {
                experssions.Add(item => item.LastName == filter.LastName);
            }

            if (filter.PhoneNumber is not null)
            {
                experssions.Add(item => item.PhoneNumber == filter.PhoneNumber);
            }

            return experssions;
        }
    }
}
