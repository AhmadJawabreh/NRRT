/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.User;
using Contracts.V1.Users.Models;
using Contracts.V1.Users.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class AuthenticationController : Controller
    {
        public readonly IUserManager _userManager;

        /// <summary>
        /// Authentication
        /// </summary>
        public AuthenticationController(IUserManager userManager) => _userManager = userManager;


        /// <summary>
        /// Login.
        /// </summary>
        /// <remarks>Login</remarks>
        /// <param name="model"></param>
        /// <response code="201">item is created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "Login_v1")]
        [ProducesResponseType(typeof(AuthenticationResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var resource = await _userManager.LogIn(model);
            return Ok(resource);
        }
    }
}
