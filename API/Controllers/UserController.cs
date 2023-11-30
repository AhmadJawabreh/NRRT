/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.User;
using Contracts.V1.Users;
using Contracts.V1.Users.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;

namespace API.Controllers
{
    /// <summary>
    /// Patients
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ReadOnly(true)]
    [Route("v1/api/users")]
    public class UserController : BaseController
    {
        public readonly IUserManager _userManager;

        /// <summary>
        /// Patients
        /// </summary>
        public UserController(IUserManager userManager) => _userManager = userManager;

        /// <summary>
        ///  Get an User by Email.
        /// </summary>
        /// <remarks>Get an User by Email.</remarks>
        /// <param name="email"></param>
        /// <response code="200">item is returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{email}", Name = "GetUserByEmail_v1")]
        [ProducesResponseType(typeof(UserResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemByEmail([FromRoute] string email)
        {
            return Ok(await _userManager.GetByEmailAsync(email));
        }

        /// <summary>
        /// Create an User.
        /// </summary>
        /// <remarks>Create an User.</remarks>
        /// <param name="model"></param>
        /// <response code="201">item is created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreateUser_v1")]
        [ProducesResponseType(typeof(UserResource), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateItem([FromBody] UserRegisterModel model)
        {
            var resource = await _userManager.CreateAsync(model);
            return Ok(resource);
        }

        /// <summary>
        /// Update an User by id.
        /// </summary>
        /// <remarks>Update an User by Id.</remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">item is updated.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}", Name = "UpdateUser_v1")]
        [ProducesResponseType(typeof(UserResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateItemAsync(string id, [FromBody] UserModificationModel model)
        {
            return Ok(await _userManager.UpdateAsync(id, model));
        }

        /// <summary>
        /// Delete an User by id.
        /// </summary>
        /// <remarks>Delete an User by id.</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">No Content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}", Name = "DeleteUser_v1")]
        [ProducesResponseType(typeof(UserResource), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteItem(string id)
        {
            await _userManager.DeleteAsync(id);
            return NoContent();
        }
    }
}
