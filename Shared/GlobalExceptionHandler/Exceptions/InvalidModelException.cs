/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Microsoft.AspNetCore.Http;

namespace Shared.GlobalExceptionHandler.Exceptions
{
    public class InvalidModelException : ValidateModelException
    {
        public InvalidModelException(string key, string error) : base() => Add(key, error);

        public override int GetStatusCode() => StatusCodes.Status400BadRequest;
    }
}
