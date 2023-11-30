/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1.PatientHistory.Filters;
using Contracts.V1.PatientHistory.Models;
using Contracts.V1.PatientHistory.Resources;
using DataAccess.SQL.Entities;
using DataAccess.SQL.UnitOfWork;
using Shared.Constants;
using Shared.GlobalExceptionHandler.Exceptions;
using System.Linq.Expressions;

namespace BusinessLogic.PatientHistory
{
    public interface IPatientHistoryManager
    {
        public Task<List<PatientHistoryResource>> GetItems(PatientHistoryListFilter filter);

        public Task<PatientHistoryResource> GetItemById(int id);

        public Task<PatientHistoryResource> CreateAsync(PatientHistoryModel companyModel);

        public Task<PatientHistoryResource> UpdateAsync(int id, PatientHistoryModel companyModel);

        public Task DeleteAsync(int id);
    }

    public class PatientHistoryManager : IPatientHistoryManager
    {
        private readonly string _userName;
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<Expression<Func<PatientHistoryEntity, object>>> _includes = new() { x => x.Patient };

        public PatientHistoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userName = string.Empty;
        }

        public async Task<List<PatientHistoryResource>> GetItems(PatientHistoryListFilter filter)
        {
            return (await _unitOfWork
                .PatientHistoryRepository
                .GetIteamsAsync(GetExpressions(filter), _includes, filter))
                .Select(item => item.ToResource()).ToList();
        }

        public async Task<PatientHistoryResource> GetItemById(int id)
        {
            return (await GetPatientHistory(id)).ToResource();
        }

        public async Task<PatientHistoryResource> CreateAsync(PatientHistoryModel model)
        {
            var patient = await _unitOfWork.PatientRepository.FirstOrDefaultAsync(x => x.Id == model.PatientId);

            if (patient is null)
            {
                throw new ItemNotFoundException(nameof(PatientEntity.Id), ValidationMessages.PatientIsNotExist(model.PatientId));
            }

            var patientHistory = await _unitOfWork.PatientHistoryRepository.FirstOrDefaultAsync(x => x.PatientId == model.PatientId);

            if (patientHistory is not null)
            {
                throw new ConflictException(nameof(PatientEntity.Id),"The Patient history is exist");
            }

            var entity = model.ToEntity(_userName);

            _unitOfWork.PatientHistoryRepository.Create(entity);

            await _unitOfWork.SaveChanges();

            return (await GetPatientHistory(entity.Id)).ToResource();
        }

        public async Task<PatientHistoryResource> UpdateAsync(int id, PatientHistoryModel model)
        {
            var entity = await GetPatientHistory(id);

            var updetedPatientHistory = model.ToEntity(_userName, id);

            updetedPatientHistory.WithCreatedInformation(entity);

            _unitOfWork.PatientHistoryRepository.Update(updetedPatientHistory);

            await _unitOfWork.SaveChanges();

            return (await GetPatientHistory(id)).ToResource();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetPatientHistory(id);

            entity.WithDeletedInformation(_userName);

            _unitOfWork.PatientHistoryRepository.Delete(entity);

            await _unitOfWork.SaveChanges();
        }

        private async Task<PatientHistoryEntity> GetPatientHistory(int id)
        {
            var entity = await _unitOfWork.PatientHistoryRepository.FirstOrDefaultAsync(item => item.Id == id, _includes);

            if (entity is null)
            {
                throw new ItemNotFoundException(nameof(PatientHistoryEntity.Id), ValidationMessages.PatientMovementIsNotExist(id));
            }

            return entity;
        }

        private static List<Expression<Func<PatientHistoryEntity, bool>>> GetExpressions(PatientHistoryListFilter filter)
        {
            List<Expression<Func<PatientHistoryEntity, bool>>> experssions = new();

            if (filter.Cancer is not null)
            {
                experssions.Add(item => item.Cancer == filter.Cancer);
            }

            if (filter.AKI is not null)
            {
                experssions.Add(item => item.AKI == filter.AKI);
            }

            if (filter.Triage is not null)
            {
                experssions.Add(item => item.Triage == filter.Triage);
            }

            if (filter.Hypertension is not null)
            {
                experssions.Add(item => item.Hypertension == filter.Hypertension);
            }

            if (filter.OpenHeartSurgery is not null)
            {
                experssions.Add(item => item.OpenHeartSurgery == filter.OpenHeartSurgery);
            }

            return experssions;
        }
    }
}
