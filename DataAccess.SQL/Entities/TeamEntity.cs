/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.SQL.Entities
{
    [Table("Teams")]
    public class TeamEntity : TrackableInformation
    {
        /// <summary>
        /// Team id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        /// Is Team Deleted?
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Team Members
        /// </summary>
        public List<TeamMemberEntity> TeamMembers { get; set; } = new List<TeamMemberEntity>();
    }
}
