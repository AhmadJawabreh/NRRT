/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using System.ComponentModel.DataAnnotations;

namespace Contracts.V1.PatientHistory.Models
{
    public class PatientHistoryModel
    {
        /// <summary>
        /// Patient Id.
        /// </summary>
        [Required]
        public int PatientId { get; set; }

        /// <summary>
        /// Patient weight
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Patient height
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Proteinuria
        /// </summary>
        public int Proteinuria { get; set; }

        /// <summary>
        /// Hematuria
        /// </summary>
        public int Hematuria { get; set; }

        /// <summary>
        /// Acute Kidney Injury biomarkers
        /// </summary>
        public int AKI { get; set; }

        /// <summary>
        /// Glomerular Giltration Rate
        /// </summary>
        public int GFR { get; set; }

        /// <summary>
        /// Does the patient have hypertension?
        /// </summary>
        public bool Hypertension { get; set; }

        /// <summary>
        /// Does the patient have kidney Disease?
        /// </summary>
        public bool KidneyDisease { get; set; }

        /// <summary>
        /// Does the patient have Heart Failure?
        /// </summary>
        public bool HeartFailure { get; set; }

        /// <summary>
        /// Does the patient have Hepatitis?
        /// </summary>
        public bool Hepatitis { get; set; }

        /// <summary>
        /// Does the patient have Regular Medications?
        /// </summary>
        public bool RegularMedications { get; set; }

        /// <summary>
        /// Does the patient have Open Heart Surgery?
        /// </summary>
        public bool OpenHeartSurgery { get; set; }
    }
}
