/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Shared.Filters;

namespace Contracts.V1
{
    public class PatientMovementListFilter : BaseFilter
    {
        /// <summary>
        /// PAtient Movement Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Patient check in.
        /// </summary>
        public DateTimeOffset? CheckIn { get; set; }

        /// <summary>
        /// Patient check out.
        /// </summary>
        public DateTimeOffset? CheckOut { get; set; }

        /// <summary>
        /// Clinic Name
        /// </summary>
        public string? ClinicName { get; set; }

        /// <summary>
        /// Specialist Name
        /// </summary>
        public string? SpecialistName { get; set; }
    }
}
