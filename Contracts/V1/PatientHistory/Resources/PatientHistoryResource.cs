/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1.Patient.Resources;
using DataAccess.SQL.Entities;
using Shared.Models;

namespace Contracts.V1.PatientHistory.Resources
{
    public class PatientHistoryResource: TrackableInformation
    {
        /// <summary>
        /// Patient History id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Patient weight
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Patient height
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Serum Ceratinine
        /// </summary>
        public int SerumCeratinine { get; set; }

        /// <summary>
        /// Anemia
        /// </summary>
        public int Anemia { get; set; }

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
        /// Does the patient have DiabetesMellitus?
        /// </summary>
        public bool DiabetesMellitus { get; set; }

        /// <summary>
        /// Does the patient have hypertension?
        /// </summary>
        public bool Hypertension { get; set; }

        /// <summary>
        /// Does the patient have Previous kidney Disease?
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
        /// Does the patient have Cancer?
        /// </summary>
        public bool Cancer { get; set; }

        /// <summary>
        /// Does the patient have Regular Medications?
        /// </summary>
        public bool RegularMedications { get; set; }

        /// <summary>
        /// Does the patient have Open Heart Surgery?
        /// </summary>
        public bool OpenHeartSurgery { get; set; }

        /// <summary>
        /// Triage
        /// </summary>
        public bool Triage { get; set; }

        /// <summary>
        /// Patient
        /// </summary>
        public PatientResource? Patient { get; set; } = default!;
    }
}
