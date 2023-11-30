/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1;
using Contracts.V1.PatientMovement.Models;
using Contracts.V1.PatientMovement.Resources;
using DataAccess.SQL.Entities;
using DataAccess.SQL.UnitOfWork;
using Shared.Constants;
using Shared.GlobalExceptionHandler.Exceptions;
using System.Linq.Expressions;


namespace BusinessLogic.PatientMovement
{
    public interface IPatientMovementManager
    {
        public Task<List<PatientMovementResource>> GetItems(PatientMovementListFilter filter);

        public Task<PatientMovementResource> GetItemById(int id);

        public Task<PatientMovementResource> CreateAsync(PatientMovementModel model);

        public Task<PatientMovementResource> UpdateAsync(int id, PatientMovementModel model);

        public Task DeleteAsync(int id);
    }

    public class PatientMovementManager : IPatientMovementManager
    {
        private readonly string _userName;
        private readonly IUnitOfWork _unitOfWork;

        public PatientMovementManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userName = string.Empty;
        }

        public async Task<List<PatientMovementResource>> GetItems(PatientMovementListFilter filter)
        {
            return (await _unitOfWork
                .PatientMovementRepository
                .GetIteamsAsync(GetExpressions(filter), null, filter))
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

        private async Task<PatientMovementEntity> GetPatientMovement(int id)
        {
            var entity = await _unitOfWork.PatientMovementRepository.FirstOrDefaultAsync(item => item.Id == id);

            if (entity is null)
            {
                throw new ItemNotFoundException(nameof(PatientMovementEntity.Id), ValidationMessages.PatientMovementIsNotExist(id));
            }

            return entity;
        }

        private static List<Expression<Func<PatientMovementEntity, bool>>> GetExpressions(PatientMovementListFilter filter)
        {
            List<Expression<Func<PatientMovementEntity, bool>>> experssions = new();

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
