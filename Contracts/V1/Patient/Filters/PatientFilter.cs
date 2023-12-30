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
        /// Patient Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Patient Identity
        /// </summary>
        public string? Identity { get; set; }

        /// <summary>
        /// Patient First Name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Patient Last Name
        /// </summary>
        public string? LastName { get; set; }

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
