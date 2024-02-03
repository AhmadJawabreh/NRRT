/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using System.ComponentModel.DataAnnotations;

namespace Contracts.V1.Team.Models
{
    public class TeamModel
    {

        /// <summary>
        /// Team Name
        /// </summary>
        [Required]
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = String.Empty;
    }
}
