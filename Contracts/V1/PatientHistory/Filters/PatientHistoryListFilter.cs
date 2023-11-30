/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Shared.Filters;

namespace Contracts.V1.PatientHistory.Filters
{
    public class PatientHistoryListFilter : BaseFilter
    {
        /// <summary>
        /// Patient Id
        /// </summary>
        public int? PatientId { get; set; }

        /// <summary>
        /// Does the patient have hypertension?
        /// </summary>
        public bool? Hypertension { get; set; }

        /// <summary>
        /// Does the patient have Cancer?
        /// </summary>
        public bool? Cancer { get; set; }

        /// Does the patient have Open Heart Surgery?
        /// </summary>
        public bool? OpenHeartSurgery { get; set; }

        /// <summary>
        /// Acute Kidney Injury biomarkers
        /// </summary>
        public int? AKI { get; set; }

        /// <summary>
        /// Triage
        /// </summary>
        public bool? Triage { get; set; }
    }
}
