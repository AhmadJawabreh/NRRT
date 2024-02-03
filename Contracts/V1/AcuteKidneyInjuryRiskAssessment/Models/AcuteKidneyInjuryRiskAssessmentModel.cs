/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

namespace Contracts.V1.AcuteKidneyInjuryRiskAssessment.Models
{
    /// <summary>
    /// Acute Kidney Injury Risk Assessment Model
    /// </summary>
    public class AcuteKidneyInjuryRiskAssessmentModel
    {
        /// <summary>
        /// Patient Id
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        ///  Does the patient has Sepsis?
        /// </summary>
        public bool HasSepsis { get; set; }

        /// <summary>
        /// Sepsis Details
        /// </summary>
        public string? SepsisDetails { get; set; }

        /// <summary>
        /// Does the patient has critical illness?
        /// </summary>
        public bool HasCriticalIllness { get; set; }

        /// <summary>
        /// Critical Illness Deatils
        /// </summary>
        public string? CriticalIllnessDeatils { get; set; }

        /// <summary>
        /// Does the patient has circulatory shock?
        /// </summary>
        public bool HasCirculatoryShock { get; set; }

        /// <summary>
        /// Circulatory Shock Deatils
        /// </summary>
        public string? CirculatoryShockDeatils { get; set; }

        /// <summary>
        /// Does the patient has Burns?
        /// </summary>
        public bool HasBurns { get; set; }

        /// <summary>
        /// Does the patient has Burns?
        /// </summary>
        public string? BurnsDeatis { get; set; }

        /// <summary>
        /// Has Trauma
        /// </summary>
        public bool Trauma { get; set; }

        /// <summary>
        /// Does the patient Has Trauma?
        /// </summary>
        public string? TraumaDeatils { get; set; }

        /// <summary>
        /// Does the patient has Cardiac Surgery?
        /// </summary>
        public bool HasCardiacSurgery { get; set; }

        /// <summary>
        /// Cardiac Surgery Deatils
        /// </summary>
        public string? CardiacSurgeryDetails { get; set; }

        /// <summary>
        /// Does the patient has major noncardiac Surgery?
        /// </summary>
        public bool HasMajorNoncardiacSurgery { get; set; }

        /// <summary>
        /// Major Noncardiac Surgery Deatils
        /// </summary>
        public string? MajorNoncardiacSurgeryDeatils { get; set; }

        /// <summary>
        /// Does the patient has Nephrotoxic Drugs?
        /// </summary>
        public bool HasNephrotoxicDrugs { get; set; }

        /// <summary>
        /// Nephrotoxic Drugs Deatils
        /// </summary>
        public string? NephrotoxicDrugsDeatils { get; set; }

        /// <summary>
        /// Does the patient has Radiocontrast Agents?
        /// </summary>
        public bool HasRadiocontrastAgents { get; set; }

        /// <summary>
        /// Radiocontrast Agents Deatils
        /// </summary>
        public string? RadiocontrastAgentsDeatils { get; set; }

        /// <summary>
        /// Does the patient has Radiocontrast Agents?
        /// </summary>
        public bool HasPoisonousPlantesAndAnimals { get; set; }

        /// <summary>
        /// Poisonous Plantes And Animals Deatils
        /// </summary>
        public string? PoisonousPlantesAndAnimalsDeatils { get; set; }

        /// <summary>
        /// Does the patient has Dehydration?
        /// </summary>
        public bool HasDehydration { get; set; }

        /// <summary>
        /// Dehydration
        /// </summary>
        public string? DehydrationsDeatils { get; set; }

        /// <summary>
        /// Does the patient has CDK?
        /// </summary>
        public bool HasCDK { get; set; }

        /// <summary>
        /// Dehydration
        /// </summary>
        public string? CDKDeatils { get; set; }

        /// <summary>
        /// Does the patient has CDK?
        /// </summary>
        public bool HasCancer { get; set; }

        /// <summary>
        /// Dehydration
        /// </summary>
        public string? CancerDeatils { get; set; }

        /// <summary>
        /// Diabetes Mellitus
        /// </summary>
        public bool DiabetesMellitus { get; set; }

        /// <summary>
        /// Does the patient has Diabetes Mellitus?
        /// </summary>
        public string? DiabetesMellitusDeatils { get; set; }

        /// <summary>
        /// Does the patient has Liver Disease?
        /// </summary>
        public bool HasLiverDisease { get; set; }

        /// <summary>
        /// Liver Disease Deatils
        /// </summary>
        public string? LiverDiseaseDeatils { get; set; }

        /// <summary>
        /// Does the patient has Heart Disease?
        /// </summary>
        public bool HasHeartDisease { get; set; }

        /// <summary>
        /// Heart Disease Deatils
        /// </summary>
        public string? HearDiseasetDeatils { get; set; }

        /// <summary>
        /// Does the patient has Heart Disease?
        /// </summary>
        public bool HasLungDisease { get; set; }

        /// <summary>
        /// Lung Disease Deatils
        /// </summary>
        public string? LungDiseaseDeatils { get; set; }

        /// <summary>
        /// Anemia
        /// </summary>
        public int Anemia { get; set; }

        /// <summary>
        /// Serum Ceratinine
        /// </summary>
        public int SerumCeratinine { get; set; }

        /// <summary>
        /// Black Race
        /// </summary>
        public bool BlackRace { get; set; }
    }
}
