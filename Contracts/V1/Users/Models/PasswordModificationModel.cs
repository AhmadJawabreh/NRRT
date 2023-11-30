/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using System.ComponentModel.DataAnnotations;

namespace Contracts.V1.Users.Models
{
    public class PasswordModificationModel
    {
        [Required]
        public string NewPassword { get; set; } = default!;

        [Required]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword do not match.")]
        public string OldPassword { get; set; } = default!;
    }
}
