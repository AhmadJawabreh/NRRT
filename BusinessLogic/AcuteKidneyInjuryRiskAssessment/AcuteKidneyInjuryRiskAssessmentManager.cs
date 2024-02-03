/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1.AcuteKidneyInjuryRiskAssessment.Filters;
using Contracts.V1.AcuteKidneyInjuryRiskAssessment.Models;
using Contracts.V1.AcuteKidneyInjuryRiskAssessment.Resources;
using DataAccess.SQL.Entities;
using DataAccess.SQL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Shared.Constants;
using Shared.GlobalExceptionHandler.Exceptions;
using System.Linq.Expressions;
using System.Security.Claims;

namespace BusinessLogic.AcuteKidneyInjuryRiskAssessment
{
    public interface IAcuteKidneyInjuryRiskAssessmentManager
    {
        public Task<List<AcuteKidneyInjuryRiskAssessmentResource>> GetItems(AcuteKidneyInjuryRiskAssessmentFilter filter);

        public Task<AcuteKidneyInjuryRiskAssessmentResource> GetItemById(int id);

        public Task<AcuteKidneyInjuryRiskAssessmentResource> CreateAsync(AcuteKidneyInjuryRiskAssessmentModel model);

        public Task<AcuteKidneyInjuryRiskAssessmentResource> UpdateAsync(int id, AcuteKidneyInjuryRiskAssessmentModel model);

        public Task DeleteAsync(int id);

        public Task<int> CountAsync();
    }

    public class AcuteKidneyInjuryRiskAssessmentManager : IAcuteKidneyInjuryRiskAssessmentManager
    {
        private readonly string _userName = default!;
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<Expression<Func<AcuteKidneyInjuryRiskAssessmentEntity, object>>> _includes = new() { x => x.Patient! };

        public AcuteKidneyInjuryRiskAssessmentManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        }

        public async Task<List<AcuteKidneyInjuryRiskAssessmentResource>> GetItems(AcuteKidneyInjuryRiskAssessmentFilter filter)
        {
            return (await _unitOfWork
               .AcuteKidneyInjuryRiskAssessmentRepository
               .GetIteamsAsync(GetExpressions(filter), _includes, filter))
               .Select(item => item.ToResource()).ToList();
        }

        public async Task<AcuteKidneyInjuryRiskAssessmentResource> CreateAsync(AcuteKidneyInjuryRiskAssessmentModel model)
        {
            var patient = await _unitOfWork.PatientRepository.FirstOrDefaultAsync(x => x.Id == model.PatientId && !x.IsDeleted);

            if (patient is null)
            {
                throw new ItemNotFoundException(nameof(PatientEntity.Id), ValidationMessages.PatientIsNotExist(model.PatientId));
            }

            var acuteKidneyInjuryRiskAssessment = await _unitOfWork.AcuteKidneyInjuryRiskAssessmentRepository.FirstOrDefaultAsync(x => x.PatientId == model.PatientId);

            if (acuteKidneyInjuryRiskAssessment is not null && !acuteKidneyInjuryRiskAssessment.IsDeleted)
            {
                throw new ConflictException(nameof(PatientEntity.Id), ValidationMessages.AcuteKidneyInjuryRiskAssessmentIsExist);
            }

            var entity = model.ToEntity(_userName);

            if (acuteKidneyInjuryRiskAssessment is not null && acuteKidneyInjuryRiskAssessment.IsDeleted)
            {
                entity.Id = acuteKidneyInjuryRiskAssessment.Id;
                _unitOfWork.AcuteKidneyInjuryRiskAssessmentRepository.Update(entity);
            }
            else
            {
                _unitOfWork.AcuteKidneyInjuryRiskAssessmentRepository.Create(entity);
            }

            await _unitOfWork.SaveChanges();

            return (await GetAcuteKidneyInjuryRiskAssessmentById(entity.Id)).ToResource();
        }

        public async Task<AcuteKidneyInjuryRiskAssessmentResource> GetItemById(int id)
        {
            return  (await GetAcuteKidneyInjuryRiskAssessmentById(id)).ToResource();
        }

        public async Task<AcuteKidneyInjuryRiskAssessmentResource> UpdateAsync(int id, AcuteKidneyInjuryRiskAssessmentModel model)
        {
            var patient = await _unitOfWork.PatientRepository.FirstOrDefaultAsync(x => x.Id == model.PatientId);

            if (patient is null)
            {
                throw new ItemNotFoundException(nameof(PatientEntity.Id), ValidationMessages.PatientIsNotExist(model.PatientId));
            }

            var AcuteKidneyInjuryRiskAssessment = await _unitOfWork.AcuteKidneyInjuryRiskAssessmentRepository.FirstOrDefaultAsync(x => x.PatientId == model.PatientId && x.Id != id && !x.IsDeleted);

            if (AcuteKidneyInjuryRiskAssessment is not null)
            {
                throw new ConflictException(nameof(PatientEntity.Id), ValidationMessages.AcuteKidneyInjuryRiskAssessmentIsExist);
            }

            var entity = await GetAcuteKidneyInjuryRiskAssessmentById(id);

            var updetedAcuteKidneyInjuryRiskAssessment = model.ToEntity(_userName, id);

            updetedAcuteKidneyInjuryRiskAssessment.WithCreatedInformation(entity);

            _unitOfWork.AcuteKidneyInjuryRiskAssessmentRepository.Update(updetedAcuteKidneyInjuryRiskAssessment);

            await _unitOfWork.SaveChanges();

            return (await GetAcuteKidneyInjuryRiskAssessmentById(id)).ToResource();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAcuteKidneyInjuryRiskAssessmentById(id);

            entity.WithDeletedInformation(_userName);

            _unitOfWork.AcuteKidneyInjuryRiskAssessmentRepository.Delete(entity);

            await _unitOfWork.SaveChanges();
        }

        public async Task<int> CountAsync()
        {
            return await _unitOfWork.AcuteKidneyInjuryRiskAssessmentRepository.CountAsync(item => !item.IsDeleted);
        }

        private async Task<AcuteKidneyInjuryRiskAssessmentEntity> GetAcuteKidneyInjuryRiskAssessmentById(int id)
        {
            var entity = await _unitOfWork.AcuteKidneyInjuryRiskAssessmentRepository.FirstOrDefaultAsync(item => item.Id == id, _includes);

            if (entity is null)
            {
                throw new ItemNotFoundException(nameof(PatientEntity.Id), ValidationMessages.AcuteKidneyInjuryRiskAssessmentIsNotExist(id));
            }

            return entity;
        }

        private static List<Expression<Func<AcuteKidneyInjuryRiskAssessmentEntity, bool>>> GetExpressions(AcuteKidneyInjuryRiskAssessmentFilter filter)
        {
            List<Expression<Func<AcuteKidneyInjuryRiskAssessmentEntity, bool>>> experssions = new() { item => !item.IsDeleted};

            if (filter.Id is not 0)
            {
                experssions.Add(item => item.Id == filter.Id);
            }

            if (filter.PatientId is not 0)
            {
                experssions.Add(item => item.PatientId == filter.PatientId);
            }

            if (filter.Triage is not null)
            {
                experssions.Add(item => item.Triage == filter.Triage);
            }

            return experssions;
        }
    }
}
