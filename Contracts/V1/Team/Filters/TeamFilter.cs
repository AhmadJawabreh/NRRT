/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using Shared.Filters;

namespace Contracts.V1.Team.Filters
{
    public class TeamFilter : BaseFilter
    {
        /// <summary>
        /// Team Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Team Name
        /// </summary>
        public string? Name { get; set; }
    }
}
