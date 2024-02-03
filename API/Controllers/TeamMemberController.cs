/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.TeamMember;
using Contracts.V1.TeamMember.Filters;
using Contracts.V1.TeamMember.Models;
using Contracts.V1.TeamMember.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;

namespace API.Controllers
{
    /// <summary>
    /// Team Members
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ReadOnly(true)]
    [Route("v1/api/teammembers")]
    public class TeamMemberController : BaseController
    {
        public readonly ITeamMemberManager _TeamMemberManager;

        /// <summary>
        /// Team Member
        /// </summary>
        public TeamMemberController(ITeamMemberManager TeamMemberManager) => _TeamMemberManager = TeamMemberManager;

        /// <summary>
        /// Get filter list of Team Members.
        /// </summary>
        /// <remarks>Get filter list of Team Members.</remarks>
        /// <param name="filter"></param>
        /// <response code="200">item are returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResourceCollection<TeamMemberResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllItems([FromQuery] TeamMemberFilter filter)
        {
            var chronometer = new Stopwatch();
            chronometer.Start();

            var totalResources = await _TeamMemberManager.CountAsync();
            var resources = await _TeamMemberManager.GetItems(filter);
            return Ok(new ResourceCollection<TeamMemberResource>(resources, totalResources, chronometer.ElapsedMilliseconds));
        }

        /// <summary>
        ///  Get a Team Member by id.
        /// </summary>
        /// <remarks>Get a Team Member by id.</remarks>
        /// <param name="id"></param>
        /// <response code="200">item is returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:min(1)}", Name = "GetTeamMemberById_v1")]
        [ProducesResponseType(typeof(TeamMemberResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
        {
            return Ok(await _TeamMemberManager.GetItemById(id));
        }

        /// <summary>
        /// Create a Team Member.
        /// </summary>
        /// <remarks>Create a Team Member.</remarks>
        /// <param name="model"></param>
        /// <response code="201">item is created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreateTeamMember_v1")]
        [ProducesResponseType(typeof(TeamMemberResource), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateItem([FromBody] TeamMemberModel model)
        {
            var resource = await _TeamMemberManager.CreateAsync(model);
            return CreatedAtAction(nameof(GetItemById), new { id = resource.Id }, resource);
        }

        /// <summary>
        /// Update a Team Member by id.
        /// </summary>
        /// <remarks>Update a Team Member by Id.</remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">item is updated.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:min(1)}", Name = "UpdateTeamMember_v1")]
        [ProducesResponseType(typeof(TeamMemberResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] TeamMemberModel model)
        {
            return Ok(await _TeamMemberManager.UpdateAsync(id, model));
        }

        /// <summary>
        /// Delete a Team Member by id.
        /// </summary>
        /// <remarks>Delete a Team Member by id.</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">No Content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:min(1)}", Name = "DeleteTeamMember_v1")]
        [ProducesResponseType(typeof(TeamMemberResource), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _TeamMemberManager.DeleteAsync(id);
            return NoContent();
        }
    }
}
