/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using System.ComponentModel.DataAnnotations;

namespace Contracts.V1.Users.Models
{
    public class UserModificationModel
    {
        [Required]
        public string UserName { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
    }
}
