/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

namespace Shared.Models
{
    /// <summary>
    /// Trackable Information
    /// </summary>
    public class TrackableInformation
    {
        /// <summary>
        /// Created By.
        /// </summary>
        public string CreatedBy { get; set; } = default!;

        /// <summary>
        /// Created On.
        /// </summary>
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Modified On
        /// </summary>
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
