﻿/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1.Patient.Filters;
using Contracts.V1.Patient.Models;
using Contracts.V1.Patient.Resources;
using DataAccess.SQL.Entities;
using DataAccess.SQL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Shared.Constants;
using Shared.GlobalExceptionHandler.Exceptions;
using System.Linq.Expressions;
using System.Security.Claims;

namespace BusinessLogic.Patient
{
    public interface IPatientManager
    {
        public Task<List<PatientResource>> GetItems(PatientFilter filter);

        public Task<PatientResource> GetItemById(int id);

        public Task<PatientResource> CreateAsync(PatientModel model);

        public Task<PatientResource> UpdateAsync(int id, PatientModel model);

        public Task DeleteAsync(int id);

        public Task<int> CountAsync();
    }

    public class PatientManager : IPatientManager
    {
        private readonly string _userName = default!;
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<Expression<Func<PatientEntity, object>>> _includes = new() { x => x.PatientHistory!, x=> x.Movements, x=> x.Team! };

        public PatientManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        }

        public async Task<List<PatientResource>> GetItems(PatientFilter filter)
        {
            return (await _unitOfWork
                .PatientRepository
                .GetIteamsAsync(GetExpressions(filter), _includes, filter))
                .Select(item => item.ToResource()).ToList();
        }

        public async Task<PatientResource> GetItemById(int id)
        {
            return (await GetPatientById(id)).ToResource();
        }

        public async Task<PatientResource> CreateAsync(PatientModel model)
        {
            var isPatientIDExist = await _unitOfWork.PatientRepository.FirstOrDefaultAsync(x=> x.Identity == model.Identity);

            if (isPatientIDExist != null)
            {
                throw new ConflictException(nameof(PatientEntity.Id), ValidationMessages.PatientIsExist(model.Identity));

            }
            var entity = model.ToEntity(_userName);

            _unitOfWork.PatientRepository.Create(entity);

            await _unitOfWork.SaveChanges();

            return (await GetPatientById(entity.Id))!.ToResource();
        }

        public async Task<PatientResource> UpdateAsync(int id, PatientModel model)
        {
            var isPatientIDExist = await _unitOfWork.PatientRepository.FirstOrDefaultAsync(x => x.Identity == model.Identity && x.Id != id);

            if (isPatientIDExist != null)
            {
                throw new ConflictException(nameof(PatientEntity.Id), ValidationMessages.PatientIsExist(model.Identity));

            }

            var entity = await GetPatientById(id);

            var updatedPatient = model.ToEntity(_userName, id);

            updatedPatient.WithCreatedInformation(entity);

            _unitOfWork.PatientRepository.Update(updatedPatient);

            await _unitOfWork.SaveChanges();

            return (await GetPatientById(entity.Id))!.ToResource();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetPatientById(id);

            await ThrowConflictExceptionWhenPatientHasOneOrMoreMovements(id);

            await ThrowConflictExceptionWhenPatientHasMedicalHistory(id);

            entity.WithDeletedInformation(_userName);

            _unitOfWork.PatientRepository.Delete(entity);

            await _unitOfWork.SaveChanges();
        }

        private async Task<PatientEntity> GetPatientById(int id)
        {
            var entity = await _unitOfWork.PatientRepository.FirstOrDefaultAsync(item => item.Id == id, _includes);

            if (entity is null)
            {
                throw new ItemNotFoundException(nameof(PatientEntity.Id), ValidationMessages.PatientIsNotExist(id));
            }

            return entity;
        }

        private async Task ThrowConflictExceptionWhenPatientHasMedicalHistory(int patientId)
        {
            var hasActiveMedicalHistory = await _unitOfWork.PatientHistoryRepository.CountAsync(item => item.PatientId == patientId && !item.IsDeleted) is not 0;

            if (hasActiveMedicalHistory)
            {
                throw new ConflictException(nameof(PatientEntity.Id), ValidationMessages.PatientHasMedicalHistory);
            }
        }

        private async Task ThrowConflictExceptionWhenPatientHasOneOrMoreMovements(int patientId)
        {
            var hasActiveMovements = await _unitOfWork.PatientMovementRepository.CountAsync(item => item.PatientId == patientId && !item.IsDeleted) is not 0;

            if (hasActiveMovements)
            {
                throw new ConflictException(nameof(PatientEntity.Id), ValidationMessages.PatientHasOneOrMoreMovements);
            }
        }

        private static List<Expression<Func<PatientEntity, bool>>> GetExpressions(PatientFilter filter)
        {
            List<Expression<Func<PatientEntity, bool>>> experssions = new();

            if(filter.Id is not 0)
            {
                experssions.Add(item => item.Id == filter.Id);
            }

            if (filter.Identity is not null)
            {
                experssions.Add(item => item.Identity == filter.Identity);
            }

            if (filter.FirstName is not null)
            {
                experssions.Add(item => item.FirstName.Contains(filter.FirstName));
            }

            if (filter.LastName is not null)
            {
                experssions.Add(item => item.FirstName.Contains(filter.LastName));
            }

            if (filter.Age is not null)
            {
                experssions.Add(item => item.Age == filter.Age);
            }

            if (filter.Gender is not null)
            {
                experssions.Add(item => item.Gender == filter.Gender);
            }

            return experssions;
        }

        public async Task<int> CountAsync()
        {
           return await _unitOfWork.PatientRepository.CountAsync(x => !x.IsDeleted);
        }
    }
}
