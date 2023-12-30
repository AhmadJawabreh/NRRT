/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

namespace Contracts.V1.Users
{
    public class UserResource
    {
        public string UserName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? Token { get; set; }

        public DateTimeOffset? ExpireAt { get; set; } = default!;

    }
}
