/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Shared.Models;

namespace Contracts.V1.PatientMovement.Resources
{
    public class PatientMovementResource: TrackableInformation
    {
        /// <summary>
        /// Patient Movement Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Patient Id.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Patient Name.
        /// </summary>
        public string PatientName { get; set; } = string.Empty;

        /// <summary>
        /// Patient Identity.
        /// </summary>
        public string PatientIdentity { get; set; } = string.Empty;

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
        public string ClinicName { get; set; } = string.Empty;

        /// <summary>
        /// Specialist Name
        /// </summary>
        public string SpecialistName { get; set; } = default!;

        /// <summary>
        /// Medical plan for this time.
        /// </summary>
        public string? MedicalPlan { get; set; }

        /// <summary>
        /// Blood Pressure.
        /// </summary>
        public int BloodPressure { get; set; }

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
    }
}
