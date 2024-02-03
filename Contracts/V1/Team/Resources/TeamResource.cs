/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1.TeamMember.Resources;
using DataAccess.SQL.Entities;
using Shared.Models;

namespace Contracts.V1.Team.Resources
{
    public class TeamResource : TrackableInformation
    {
        /// <summary>
        /// Team id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Team Name
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = String.Empty;

        /// <summary>
        /// Team Members
        /// </summary>
        public List<TeamMemberResource> TeamMember { get; set; } = new List<TeamMemberResource>();
    }
}
