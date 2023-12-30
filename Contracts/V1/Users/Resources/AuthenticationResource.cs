/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

namespace Contracts.V1.Users.Resources
{
    public class AuthenticationResource
    {
        public string? Token { get; set; }
        public DateTimeOffset ExpireAt { get; set; }
    }
}
