/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Microsoft.AspNetCore.Http;

namespace Shared.GlobalExceptionHandler.Exceptions
{
    public class ItemNotFoundException : ValidateModelException
    {
        public ItemNotFoundException(string key, string message) : base() => Add(key, message);

        public override int GetStatusCode() => StatusCodes.Status404NotFound;
    }
}
