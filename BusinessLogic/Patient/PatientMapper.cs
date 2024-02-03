/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.PatientHistory;
using BusinessLogic.PatientMovement;
using BusinessLogic.Team;
using Contracts.V1.Patient.Models;
using Contracts.V1.Patient.Resources;
using DataAccess.SQL.Entities;

namespace BusinessLogic.Patient
{
    public static class PatientMapper
    {
        public static PatientResource ToResource(this PatientEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Identity = entity.Identity,
                FirstName = entity.FirstName,
                FatherName = entity.FatherName,
                GrandFatherName = entity.GrandFatherName,
                FamilyName = entity.FamilyName,
                Age = entity.Age,
                Gender = entity.Gender,
                Address = entity.Address,
                Religion = entity.Religion,
                MonthlyIncome = entity.MonthlyIncome,
                PhoneNumber = entity.PhoneNumber,
                EducationLevel = entity.EducationLevel,
                Team = entity.Team!.ToResource(),
                Movements = entity.Movements.Select(item=> item.ToResource()).ToList(),
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                ModifiedBy = entity.ModifiedBy,
                ModifiedOn = entity.ModifiedOn,
            };
        }

        public static PatientEntity ToEntity(this PatientModel model, string userName, int id = 0)
        {
            return new PatientEntity()
            {
                Id = id,
                TeamId = model.TeamId,
                Identity = model.Identity,
                FirstName = model.FirstName,
                FatherName = model.FatherName,
                GrandFatherName = model.GrandFatherName,
                FamilyName = model.FamilyName,
                Age = model.Age,
                PhoneNumber = model.PhoneNumber,
                EducationLevel = model.EducationLevel,
                Gender = model.Gender,
                Address = model.Address,
                Religion = model.Religion,
                MonthlyIncome = model.MonthlyIncome,
            }.WithTracableInformation(userName, id == 0);
        }

        public static PatientEntity WithTracableInformation(this PatientEntity source, string userName, bool isNewEntity = false)
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

        public static void WithCreatedInformation(this PatientEntity destination, PatientEntity source)
        {
            destination.CreatedBy = source.CreatedBy;
            destination.CreatedOn = source.CreatedOn;
        }

        public static void WithDeletedInformation(this PatientEntity destination, string userName)
        {
            destination.IsDeleted = true;
            destination.ModifiedBy = userName;
            destination.ModifiedOn = DateTimeOffset.UtcNow;
        }
    }
}
