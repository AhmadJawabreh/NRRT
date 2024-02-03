/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1.Team.Resources;
using DataAccess.SQL.Entities;
using Shared.Models;

namespace Contracts.V1.TeamMember.Resources
{
    public class TeamMemberResource : TrackableInformation
    {
        /// <summary>
        /// Team Member id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Father Name
        /// </summary>
        public string FatherName { get; set; } = string.Empty;

        /// <summary>
        /// Grand Father Name
        /// </summary>
        public string GrandFatherName { get; set; } = string.Empty;

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Posistion
        /// </summary>
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// Phone Number
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;


        /// <summary>
        /// More Details about the Team Member
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Is Deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Team Entity
        /// </summary>
        public TeamResource? Team { get; set; }
    }
}
