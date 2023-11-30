/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using System.ComponentModel.DataAnnotations;

namespace Contracts.V1.PatientMovement.Models
{
    public class PatientMovementModel
    {

        /// <summary>
        /// Patient id.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Patient check in.
        /// </summary>
        public DateTimeOffset CheckIn { get; set; }

        /// <summary>
        /// Patient check out.
        /// </summary>
        public DateTimeOffset? CheckOut { get; set; }

        /// <summary>
        /// Clinic Name
        /// </summary>
        [Required]
        public string ClinicName { get; set; } = string.Empty;

        /// <summary>
        /// Specialist Name
        /// </summary>
        [Required]
        public string SpecialistName { get; set; } = default!;

        /// <summary>
        /// Medical plan for this time.
        /// </summary>
        [Required]
        public string MedicalPlan { get; set; } = string.Empty;

        /// <summary>
        /// Blood Pressure.
        /// </summary>
        [Required]
        public int BloodPressure { get; set; }

        /// <summary>
        /// Heart Beats
        /// </summary>
        [Required]
        public int HeartBeats { get; set; }

        /// <summary>
        /// Did the patient Have Albumin?
        /// </summary>
        [Required]
        public bool HaveEdema { get; set; }

        /// <summary>
        /// Did the patient take the Contrast Media?
        /// </summary>
        [Required]
        public bool HaveContrastMedia { get; set; }

        /// <summary>
        /// Did the patient have the Cardac Catherterization?
        /// </summary>
        [Required]
        public bool HaveCardacCatherterization { get; set; }

        /// <summary>
        /// Did the patient have drugs?
        /// </summary>
        [Required]

        public bool TakeDrugs { get; set; }

        /// <summary>
        /// Drugs
        /// </summary>
        public string Drugs { get; set; } = default!;
    }
}
