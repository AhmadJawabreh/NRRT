/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace Contracts.V1.Patient.Models
{
    public class PatientModel
    {
        /// <summary>
        /// Team Id
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Patient First Name
        /// </summary>
        [Required]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Patient First Name
        /// </summary>
        [Required]
        public string FatherName{ get; set; } = string.Empty;

        /// <summary>
        /// Patient First Name
        /// </summary>
        [Required]
        public string GrandFatherName { get; set; } = string.Empty;

        /// <summary>
        /// Patient First Name
        /// </summary>
        [Required]
        public string FamilyName { get; set; } = string.Empty;

        /// <summary>
        /// Patient Identity
        /// </summary>
        public string Identity { get; set; } = string.Empty;

        /// <summary>
        /// Patient Age
        /// </summary>
        [Required]
        public int Age { get; set; }
        /// <summary>
        /// Patient monthly .ncome
        /// </summary>
        [Required]
        public int MonthlyIncome { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Phone Number
        /// </summary>
        public LevelsEducations EducationLevel { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [Required]
        public Gender Gender { get; set; }

        /// <summary>
        /// Patient Religion
        /// </summary>
        [Required]
        public Religion Religion { get; set; }

        /// <summary>
        /// Pateint location
        /// </summary>
        [Required]
        public string Address { get; set; } = string.Empty;
    }
}
