/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Shared.Enum;
using Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.SQL.Entities
{
    /// <summary>
    /// Patient
    /// </summary>
    [Table("Patients")]
    public class PatientEntity : TrackableInformation
    {
        /// <summary>
        /// Patient id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        /// Is Deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Patient History Id
        /// </summary>
        public int? PatientHistoryId { get; set; }
        public PatientHistoryEntity? PatientHistory { get; set; }

        /// <summary>
        /// The Patient Movements.
        /// </summary>
        public List<PatientMovementEntity> Movements { get; set; } = Enumerable.Empty<PatientMovementEntity>().ToList();
    }
}
