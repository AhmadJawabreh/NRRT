/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Shared.Enum;
using Shared.Filters;

namespace Contracts.V1.Patient.Filters
{
    public class PatientFilter : BaseFilter
    {
        /// <summary>
        /// Patient Identity
        /// </summary>
        public string? Identity { get; set; }

        /// <summary>
        /// Patient Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Patient Age
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public Gender? Gender { get; set; }
    }
}
