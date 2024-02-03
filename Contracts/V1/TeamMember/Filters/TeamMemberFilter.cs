/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using Shared.Filters;

namespace Contracts.V1.TeamMember.Filters
{
    public class TeamMemberFilter : BaseFilter
    {
        /// <summary>
        /// Team Member Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Team Id.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Father Name
        /// </summary>
        public string? FatherName { get; set; }

        /// <summary>
        /// Grand Father Name
        /// </summary>
        public string? GrandFatherName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Posistion
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}
