/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using Shared.Filters;

namespace Contracts.V1.AcuteKidneyInjuryRiskAssessment.Filters
{
    public class AcuteKidneyInjuryRiskAssessmentFilter : BaseFilter
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Patient Id
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Triage
        /// </summary>
        public bool? Triage { get; set; }
    }
}
