/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.SQL.Entities
{
    /// <summary>
    /// Patient Movements
    /// </summary>
    [Table("PatientMovements")]
    public class PatientMovementEntity : TrackableInformation
    {
        /// <summary>
        /// Patient Movement id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Patient check in.
        /// </summary>
        public DateTimeOffset CheckIn { get; set; }

        /// <summary>
        /// Patient check out.
        /// </summary>
        public DateTimeOffset? CheckOut { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// Specialist Name
        /// </summary>
        public string SpecialistName { get; set; } = string.Empty;

        /// <summary>
        /// Medical plan for this time.
        /// </summary>
        public string? MedicalPlan { get; set; }

        /// <summary>
        /// Blood Pressure.
        /// </summary>
        public string BloodPressure { get; set; } = default!;

        /// <summary>
        /// Heart Beats
        /// </summary>
        public int HeartBeats { get; set; }

        /// <summary>
        /// Did the patient Have Albumin?
        /// </summary>
        public bool HaveEdema { get; set; }

        /// <summary>
        /// Did the patient take the Contrast Media?
        /// </summary>
        public bool HaveContrastMedia { get; set; }

        /// <summary>
        /// Did the patient have the Cardac Catherterization?
        /// </summary>
        public bool HaveCardacCatherterization { get; set; }

        /// <summary>
        /// Did the patient have drugs?
        /// </summary>
        public bool TakeDrugs { get; set; }

        /// <summary>
        /// Drugs
        /// </summary>
        public string? Drugs { get; set; }

        /// <summary>
        /// Patient Id.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Patient
        /// </summary>
        public PatientEntity Patient { get; set; } = default!;

        /// <summary>
        /// Is Deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
