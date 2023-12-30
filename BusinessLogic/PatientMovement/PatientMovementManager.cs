/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1;
using Contracts.V1.PatientMovement.Models;
using Contracts.V1.PatientMovement.Resources;
using DataAccess.SQL.Entities;
using DataAccess.SQL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Shared.Constants;
using Shared.GlobalExceptionHandler.Exceptions;
using System.Linq.Expressions;
using System.Security.Claims;

namespace BusinessLogic.PatientMovement
{
    public interface IPatientMovementManager
    {
        public Task<List<PatientMovementResource>> GetItems(PatientMovementListFilter filter);

        public Task<PatientMovementResource> GetItemById(int id);

        public Task<PatientMovementResource> CreateAsync(PatientMovementModel model);

        public Task<PatientMovementResource> UpdateAsync(int id, PatientMovementModel model);

        public Task DeleteAsync(int id);

        public Task<int> CountAsync();

    }

    public class PatientMovementManager : IPatientMovementManager
    {
        private readonly string _userName;
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<Expression<Func<PatientMovementEntity, object>>> _includes = new() { x => x.Patient };

        public PatientMovementManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        }

        public async Task<List<PatientMovementResource>> GetItems(PatientMovementListFilter filter)
        {
            return (await _unitOfWork
                .PatientMovementRepository
                .GetIteamsAsync(GetExpressions(filter), _includes, filter))
                .Select(item => item.ToResource()).ToList();
        }

        public async Task<PatientMovementResource> GetItemById(int id)
        {
            return (await GetPatientMovement(id)).ToResource();
        }

        public async Task<PatientMovementResource> CreateAsync(PatientMovementModel model)
        {
            var entity = model.ToEntity(_userName);

            _unitOfWork.PatientMovementRepository.Create(entity);

            await _unitOfWork.SaveChanges();

            return (await GetPatientMovement(entity.Id)).ToResource();
        }

        public async Task<PatientMovementResource> UpdateAsync(int id, PatientMovementModel model)
        {
            var entity = await GetPatientMovement(id);

            var updatedPatientMovement = model.ToEntity(_userName, id);

            updatedPatientMovement.WithCreatedInformation(entity);

            _unitOfWork.PatientMovementRepository.Update(updatedPatientMovement);

            await _unitOfWork.SaveChanges();

            return (await GetPatientMovement(id)).ToResource();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetPatientMovement(id);

            entity.WithDeletedInformation(_userName);

            _unitOfWork.PatientMovementRepository.Delete(entity);

            await _unitOfWork.SaveChanges();
        }

        public async Task<int> CountAsync()
        {
            return await _unitOfWork.PatientMovementRepository.CountAsync(x => !x.IsDeleted);
        }

        private async Task<PatientMovementEntity> GetPatientMovement(int id)
        {
            var entity = await _unitOfWork.PatientMovementRepository.FirstOrDefaultAsync(item => item.Id == id, _includes);

            if (entity is null)
            {
                throw new ItemNotFoundException(nameof(PatientMovementEntity.Id), ValidationMessages.PatientMovementIsNotExist(id));
            }

            return entity;
        }

        private static List<Expression<Func<PatientMovementEntity, bool>>> GetExpressions(PatientMovementListFilter filter)
        {
            List<Expression<Func<PatientMovementEntity, bool>>> experssions = new();

            if(filter.Id is not 0)
            {
                experssions.Add(item => item.Id == filter.Id);
            }

            if (filter.CheckIn is not null)
            {
                experssions.Add(item => item.CheckIn == filter.CheckIn);
            }

            if (filter.CheckOut is not null)
            {
                experssions.Add(item => item.CheckOut == filter.CheckOut);
            }

            if (filter.CheckOut is not null)
            {
                experssions.Add(item => item.CheckOut == filter.CheckOut);
            }

            if (filter.ClinicName is not null)
            {
                experssions.Add(item => item.ClinicName == filter.ClinicName);
            }

            if (filter.SpecialistName is not null)
            {
                experssions.Add(item => item.SpecialistName == filter.SpecialistName);
            }

            return experssions;
        }
    }
}
