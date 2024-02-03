/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.Patient;
using Contracts.V1.AcuteKidneyInjuryRiskAssessment.Models;
using Contracts.V1.AcuteKidneyInjuryRiskAssessment.Resources;
using DataAccess.SQL.Entities;

namespace BusinessLogic.AcuteKidneyInjuryRiskAssessment
{
    public static class AcuteKidneyInjuryRiskAssessmentMapper
    {
        public static AcuteKidneyInjuryRiskAssessmentResource ToResource(this AcuteKidneyInjuryRiskAssessmentEntity entity)
        {
            return new()
            {
                Id = entity.Id,
                Patient = entity.Patient.ToResource(),
                Anemia = entity.Anemia,
                SerumCeratinine = entity.SerumCeratinine,
                BlackRace = entity.BlackRace,
                BurnsDeatis = entity.BurnsDeatis,
                CancerDeatils = entity.CancerDeatils,
                CDKDeatils = entity.CDKDeatils,
                CirculatoryShockDeatils = entity.CirculatoryShockDeatils,
                CriticalIllnessDeatils = entity.CriticalIllnessDeatils,
                DehydrationsDeatils = entity.DehydrationsDeatils,
                DiabetesMellitusDeatils = entity.DiabetesMellitusDeatils,
                HasBurns = entity.HasBurns,
                HasCancer = entity.HasCancer,
                HasCardiacSurgery = entity.HasCardiacSurgery,
                CardiacSurgeryDetails = entity.CardiacSurgeryDetails,
                HasCDK = entity.HasCDK,
                HasCirculatoryShock = entity.HasCirculatoryShock,
                HasCriticalIllness = entity.HasCriticalIllness,
                HasDehydration = entity.HasDehydration,
                HasHeartDisease = entity.HasHeartDisease,
                HasLiverDisease = entity.HasLiverDisease,
                HasLungDisease = entity.HasLungDisease,
                HasMajorNoncardiacSurgery = entity.HasMajorNoncardiacSurgery,
                HasNephrotoxicDrugs = entity.HasNephrotoxicDrugs,
                HasPoisonousPlantesAndAnimals = entity.HasPoisonousPlantesAndAnimals,
                HasRadiocontrastAgents = entity.HasRadiocontrastAgents,
                HasSepsis = entity.HasSepsis,
                DiabetesMellitus = entity.DiabetesMellitus,
                HearDiseasetDeatils = entity.HearDiseasetDeatils,
                LiverDiseaseDeatils = entity.LiverDiseaseDeatils,
                LungDiseaseDeatils = entity.LungDiseaseDeatils,
                MajorNoncardiacSurgeryDeatils = entity.MajorNoncardiacSurgeryDeatils,
                NephrotoxicDrugsDeatils = entity.NephrotoxicDrugsDeatils,
                PoisonousPlantesAndAnimalsDeatils = entity.PoisonousPlantesAndAnimalsDeatils,
                RadiocontrastAgentsDeatils = entity.RadiocontrastAgentsDeatils,
                Trauma = entity.Trauma,
                TraumaDeatils = entity.TraumaDeatils,
                SepsisDetails = entity.SepsisDetails,
                Triage = entity.Triage,
                CreatedBy = entity.CreatedBy,
                CreatedOn = entity.CreatedOn,
                ModifiedBy = entity.ModifiedBy,
                ModifiedOn = entity.ModifiedOn
            };
        }

        public static AcuteKidneyInjuryRiskAssessmentEntity ToEntity(this AcuteKidneyInjuryRiskAssessmentModel model, string userName, int id = 0)
        {
            return new AcuteKidneyInjuryRiskAssessmentEntity()
            {
                Id = id,
                PatientId = model.PatientId,
                Anemia = model.Anemia,
                SerumCeratinine = model.SerumCeratinine,
                BlackRace = model.BlackRace,
                BurnsDeatis = model.BurnsDeatis,
                CancerDeatils = model.CancerDeatils,
                CDKDeatils = model.CDKDeatils,
                CirculatoryShockDeatils = model.CirculatoryShockDeatils,
                CriticalIllnessDeatils = model.CriticalIllnessDeatils,
                DehydrationsDeatils = model.DehydrationsDeatils,
                DiabetesMellitusDeatils = model.DiabetesMellitusDeatils,
                HasBurns = model.HasBurns,
                HasCancer = model.HasCancer,
                HasCardiacSurgery = model.HasCardiacSurgery,
                CardiacSurgeryDetails = model.CardiacSurgeryDetails,
                HasCDK = model.HasCDK,
                HasCirculatoryShock = model.HasCirculatoryShock,
                HasCriticalIllness = model.HasCriticalIllness,
                HasDehydration = model.HasDehydration,
                HasHeartDisease = model.HasHeartDisease,
                HasLiverDisease = model.HasLiverDisease,
                HasLungDisease = model.HasLungDisease,
                HasMajorNoncardiacSurgery = model.HasMajorNoncardiacSurgery,
                HasNephrotoxicDrugs = model.HasNephrotoxicDrugs,
                HasPoisonousPlantesAndAnimals = model.HasPoisonousPlantesAndAnimals,
                HasRadiocontrastAgents = model.HasRadiocontrastAgents,
                HasSepsis = model.HasSepsis,
                DiabetesMellitus = model.DiabetesMellitus,
                HearDiseasetDeatils = model.HearDiseasetDeatils,
                LiverDiseaseDeatils = model.LiverDiseaseDeatils,
                LungDiseaseDeatils = model.LungDiseaseDeatils,
                MajorNoncardiacSurgeryDeatils = model.MajorNoncardiacSurgeryDeatils,
                NephrotoxicDrugsDeatils = model.NephrotoxicDrugsDeatils,
                PoisonousPlantesAndAnimalsDeatils = model.PoisonousPlantesAndAnimalsDeatils,
                RadiocontrastAgentsDeatils = model.RadiocontrastAgentsDeatils,
                Trauma = model.Trauma,
                TraumaDeatils = model.TraumaDeatils,
                SepsisDetails = model.SepsisDetails,
                IsDeleted = false
            }.WithTracableInformation(userName, id == 0);
        }

        public static AcuteKidneyInjuryRiskAssessmentEntity WithTracableInformation(this AcuteKidneyInjuryRiskAssessmentEntity source, string userName, bool isNewEntity = false)
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

        public static void WithCreatedInformation(this AcuteKidneyInjuryRiskAssessmentEntity destination, AcuteKidneyInjuryRiskAssessmentEntity source)
        {
            destination.CreatedBy = source.CreatedBy;
            destination.CreatedOn = source.CreatedOn;
        }

        public static void WithDeletedInformation(this AcuteKidneyInjuryRiskAssessmentEntity destination, string userName)
        {
            destination.IsDeleted = true;
            destination.ModifiedBy = userName;
            destination.ModifiedOn = DateTimeOffset.UtcNow;
        }
    }
}
