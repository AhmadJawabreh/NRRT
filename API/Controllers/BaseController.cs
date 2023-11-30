/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.SharedModels;

namespace API.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class BaseController : Controller
    {
    }
}
