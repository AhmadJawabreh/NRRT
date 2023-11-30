﻿/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Microsoft.AspNetCore.Http;

namespace Shared.GlobalExceptionHandler
{
    public abstract class ValidateModelException : ApplicationException
    {
        public Dictionary<string, HashSet<string>> Errors { get; set; } = new();

        protected ValidateModelException() : base() { }

        public void Add(string key, string value)
        {
            if (Errors.ContainsKey(key))
                Errors[key].Append(value);
            else
                Errors.Add(key, new HashSet<string> { value });
        }

        public object? GetErrors() => Errors;

        public abstract int GetStatusCode();
    }
}
