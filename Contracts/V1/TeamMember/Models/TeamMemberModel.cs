/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using System.ComponentModel.DataAnnotations;


namespace Contracts.V1.TeamMember.Models
{
    public class TeamMemberModel
    {
        /// <summary>
        /// Team Id.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [Required]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Father Name
        /// </summary>
        [Required]
        public string FatherName { get; set; } = string.Empty;

        /// <summary>
        /// Grand Father Name
        /// </summary>
        [Required]
        public string GrandFatherName { get; set; } = string.Empty;

        /// <summary>
        /// Last Name
        /// </summary>
        [Required]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Posistion
        /// </summary>
        [Required]
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// Phone Number
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;


        /// <summary>
        /// More Details about the Team Member
        /// </summary>
        public string? Description { get; set; }
    }
}
