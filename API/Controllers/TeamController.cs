/* 
 * Copyright (C) 2024 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.Team;
using Contracts.V1.Team.Filters;
using Contracts.V1.Team.Models;
using Contracts.V1.Team.Resources;
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
    /// Teams
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ReadOnly(true)]
    [Route("v1/api/teams")]
    public class TeamController : BaseController
    {
        public readonly ITeamManager _teamManager;

        /// <summary>
        /// Teams
        /// </summary>
        public TeamController(ITeamManager teamManager) => _teamManager = teamManager;

        /// <summary>
        /// Get filter list of teams.
        /// </summary>
        /// <remarks>Get filter list of teams.</remarks>
        /// <param name="filter"></param>
        /// <response code="200">item are returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResourceCollection<TeamResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllItems([FromQuery] TeamFilter filter)
        {
            var chronometer = new Stopwatch();
            chronometer.Start();

            var totalResources = await _teamManager.CountAsync();
            var resources = await _teamManager.GetItems(filter);
            return Ok(new ResourceCollection<TeamResource>(resources, totalResources, chronometer.ElapsedMilliseconds));
        }

        /// <summary>
        ///  Get a Team by id.
        /// </summary>
        /// <remarks>Get a Team by id.</remarks>
        /// <param name="id"></param>
        /// <response code="200">item is returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:min(1)}", Name = "GetTeamById_v1")]
        [ProducesResponseType(typeof(TeamResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
        {
            return Ok(await _teamManager.GetItemById(id));
        }

        /// <summary>
        /// Create a Team.
        /// </summary>
        /// <remarks>Create a Team.</remarks>
        /// <param name="model"></param>
        /// <response code="201">item is created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreateTeam_v1")]
        [ProducesResponseType(typeof(TeamResource), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateItem([FromBody] TeamModel model)
        {
            var resource = await _teamManager.CreateAsync(model);
            return CreatedAtAction(nameof(GetItemById), new { id = resource.Id }, resource);
        }

        /// <summary>
        /// Update a Team by id.
        /// </summary>
        /// <remarks>Update a Team by Id.</remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">item is updated.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:min(1)}", Name = "UpdateTeam_v1")]
        [ProducesResponseType(typeof(TeamResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] TeamModel model)
        {
            return Ok(await _teamManager.UpdateAsync(id, model));
        }

        /// <summary>
        /// Delete a Team by id.
        /// </summary>
        /// <remarks>Delete a Team by id.</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">No Content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:min(1)}", Name = "DeleteTeam_v1")]
        [ProducesResponseType(typeof(TeamResource), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _teamManager.DeleteAsync(id);
            return NoContent();
        }
    }
}
