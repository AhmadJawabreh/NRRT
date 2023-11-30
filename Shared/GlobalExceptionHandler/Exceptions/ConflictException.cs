/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Microsoft.AspNetCore.Http;

namespace Shared.GlobalExceptionHandler.Exceptions
{
    public class ConflictException : ValidateModelException
    {
        public ConflictException(string key, string error) : base() => Add(key, error);

        public override int GetStatusCode() => StatusCodes.Status409Conflict;
    }
}
