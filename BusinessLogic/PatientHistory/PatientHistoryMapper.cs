﻿
/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.Patient;
using Contracts.V1.PatientHistory.Models;
using Contracts.V1.PatientHistory.Resources;
using DataAccess.SQL.Entities;

namespace BusinessLogic.PatientHistory
{
    public static class PatientHistoryMapper
    {
        public static PatientHistoryResource ToResource(this PatientHistoryEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                AKI = entity.AKI,
                Weight = entity.Weight,
                Height = entity.Height,
                GFR = entity.GFR,
                HeartFailure = entity.HeartFailure,
                Hematuria = entity.Hematuria,
                Hepatitis = entity.Hepatitis,
                Hypertension = entity.Hypertension,
                OpenHeartSurgery = entity.OpenHeartSurgery,
                Patient = entity.Patient?.ToResource(),
                KidneyDisease = entity.KidneyDisease,
                Proteinuria = entity.Proteinuria,
                RegularMedications = entity.RegularMedications,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                ModifiedBy = entity.ModifiedBy,
                ModifiedOn = entity.ModifiedOn,
            };
        }

        public static PatientHistoryEntity ToEntity(this PatientHistoryModel model, string userName, int id = 0)
        {
            return new PatientHistoryEntity()
            {
                Id = id,
                PatientId = model.PatientId,
                AKI = model.AKI,
                Weight = model.Weight,
                Height = model.Height,
                GFR = model.GFR,
                HeartFailure = model.HeartFailure,
                Hematuria = model.Hematuria,
                Hepatitis = model.Hepatitis,
                Hypertension = model.Hypertension,
                OpenHeartSurgery = model.OpenHeartSurgery,
                KidneyDisease = model.KidneyDisease,
                Proteinuria = model.Proteinuria,
                RegularMedications = model.RegularMedications,
                IsDeleted = false
            }.WithTracableInformation(userName, id == 0);
        }

        public static PatientHistoryEntity WithTracableInformation(this PatientHistoryEntity source, string userName, bool isNewEntity = false)
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

        public static void WithCreatedInformation(this PatientHistoryEntity destination, PatientHistoryEntity source)
        {
            destination.CreatedBy = source.CreatedBy;
            destination.CreatedOn = source.CreatedOn;
        }

        public static void WithDeletedInformation(this PatientHistoryEntity destination, string userName)
        {
            destination.IsDeleted = true;
            destination.ModifiedBy = userName;
            destination.ModifiedOn = DateTimeOffset.UtcNow;
        }
    }
}
