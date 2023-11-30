/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1.PatientMovement.Models;
using Contracts.V1.PatientMovement.Resources;
using DataAccess.SQL.Entities;

namespace BusinessLogic.PatientMovement
{
    public static class PatientMovementMapper
    {
        public static PatientMovementResource ToResource(this PatientMovementEntity entity)
        {
            return new()
            {
                Id = entity.Id,                 
                ClinicName = entity.ClinicName,
                SpecialistName = entity.SpecialistName,
                CheckIn = entity.CheckIn,
                CheckOut = entity.CheckOut,
                HaveContrastMedia = entity.HaveContrastMedia,
                HaveEdema = entity.HaveEdema,
                BloodPressure = entity.BloodPressure,
                HeartBeats = entity.HeartBeats,
                MedicalPlan = entity.MedicalPlan,
                HaveCardacCatherterization = entity.HaveCardacCatherterization,
                Drugs = entity.Drugs,
                TakeDrugs = entity.TakeDrugs,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                ModifiedBy = entity.ModifiedBy,
                ModifiedOn = entity.ModifiedOn,
            };
        }

        public static PatientMovementEntity ToEntity(this PatientMovementModel model, string userName, int id = 0)
        {
            return new PatientMovementEntity()
            {
                Id = id,
                PatientId = model.PatientId,
                ClinicName = model.ClinicName,
                SpecialistName = model.SpecialistName,
                CheckIn = model.CheckIn,
                CheckOut = model.CheckOut,
                HaveContrastMedia = model.HaveContrastMedia,
                HaveEdema = model.HaveEdema,
                BloodPressure = model.BloodPressure,
                HeartBeats = model.HeartBeats,
                MedicalPlan = model.MedicalPlan,
                HaveCardacCatherterization = model.HaveCardacCatherterization,
                Drugs = model.Drugs,
            }.WithTracableInformation(userName, id == 0);
        }

        public static PatientMovementEntity WithTracableInformation(this PatientMovementEntity source, string userName, bool isNewEntity = false)
        {
            if(isNewEntity)
            {
                source.CreatedBy = userName;
                source.CreatedOn = DateTimeOffset.UtcNow;
            }

            source.ModifiedBy = userName;
            source.ModifiedOn = DateTimeOffset.UtcNow;

            return source;
        }

        public static void WithCreatedInformation(this PatientMovementEntity destination, PatientMovementEntity source)
        {
            destination.CreatedBy = source.CreatedBy;
            destination.CreatedOn = source.CreatedOn;
        }

        public static void WithDeletedInformation(this PatientMovementEntity destination, string userName)
        {
            destination.IsDeleted = true;
            destination.ModifiedBy = userName;
            destination.ModifiedOn = DateTimeOffset.UtcNow;
        }
    }
}
