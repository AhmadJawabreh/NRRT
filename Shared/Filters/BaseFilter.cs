/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using System.ComponentModel.DataAnnotations;

namespace Shared.Filters
{
    /// <summary>
    /// Filter.
    /// </summary>
    public class BaseFilter
    {
        /// <summary>
        /// Skip Items.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int? Skip { get; set; }

        /// <summary>
        /// Take Items.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int? Take { get; set; }
    }
}
