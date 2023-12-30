/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1.PatientMovement.Resources;
using Shared.Enum;
using Shared.Models;

namespace Contracts.V1.Patient.Resources
{
    public class PatientResource : TrackableInformation
    {
        /// <summary>
        /// Patient id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Patient Identity
        /// </summary>
        public string Identity { get; set; } = string.Empty;

        /// <summary>
        /// Patient First Name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Patient First Name
        /// </summary>
        public string FatherName { get; set; } = string.Empty;

        /// <summary>
        /// Patient First Name
        /// </summary>
        public string GrandFatherName { get; set; } = string.Empty;

        /// <summary>
        /// Patient First Name
        /// </summary>
        public string FamilyName { get; set; } = string.Empty;

        /// <summary>
        /// Patient Age
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Patient monthly .ncome
        /// </summary>
        public int MonthlyIncome { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Patient Religion
        /// </summary>
        public Religion Religion { get; set; }

        /// <summary>
        /// Pateint location
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Patient Movements
        /// </summary>
        public List<PatientMovementResource> Movements { get; set; } = new List<PatientMovementResource>();
    }
}
