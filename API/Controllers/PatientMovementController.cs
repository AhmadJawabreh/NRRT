/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.PatientMovement;
using Contracts.V1;
using Contracts.V1.PatientMovement.Models;
using Contracts.V1.PatientMovement.Resources;
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
    /// Patients Movements
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ReadOnly(true)]
    [Route("v1/api/patienstmovements")]
    public class PatientMovementController : BaseController
    {
        public readonly IPatientMovementManager _patientMovementManager;

        /// <summary>
        /// Patients Movements
        /// </summary>
        public PatientMovementController(IPatientMovementManager patientMovementManager) => _patientMovementManager = patientMovementManager;

        /// <summary>
        /// Get filter list of Patient Movement.
        /// </summary>
        /// <remarks>Get filter list of Patient Movement.</remarks>
        /// <param name="filter"></param>
        /// <response code="200">item are returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResourceCollection<PatientMovementResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllItems([FromQuery] PatientMovementListFilter filter)
        {
            var chronometer = new Stopwatch();
            chronometer.Start();

            var resources = await _patientMovementManager.GetItems(filter);
            return Ok(new ResourceCollection<PatientMovementResource>(resources, resources.Count, chronometer.ElapsedMilliseconds));
        }

        /// <summary>
        ///  Get a Patient Movement by id.
        /// </summary>
        /// <remarks>Get a Patient Movement by id.</remarks>
        /// <param name="id"></param>
        /// <response code="200">item is returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:min(1)}", Name = "GetPatientMovementById_v1")]
        [ProducesResponseType(typeof(PatientMovementResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
        {
            return Ok(await _patientMovementManager.GetItemById(id));
        }

        /// <summary>
        /// Create a Patient Movement.
        /// </summary>
        /// <remarks>Create a Patient Movement.</remarks>
        /// <param name="model"></param>
        /// <response code="201">item is created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreatePatientMovement_v1")]
        [ProducesResponseType(typeof(PatientMovementResource), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateItem([FromBody] PatientMovementModel model)
        {
            var resource = await _patientMovementManager.CreateAsync(model);
            return CreatedAtAction(nameof(GetItemById), new { id = resource.Id }, resource);
        }

        /// <summary>
        /// Update a Patient Movement by id.
        /// </summary>
        /// <remarks>Update a Patient Movement by Id.</remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">item is updated.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:min(1)}", Name = "UpdatePatientMovement_v1")]
        [ProducesResponseType(typeof(PatientMovementResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] PatientMovementModel model)
        {
            return Ok(await _patientMovementManager.UpdateAsync(id, model));
        }

        /// <summary>
        /// Delete a Patient Movement by id.
        /// </summary>
        /// <remarks>Delete a Patient Movement by id.</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">No Content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:min(1)}", Name = "DeletePatientMovement_v1")]
        [ProducesResponseType(typeof(PatientMovementResource), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _patientMovementManager.DeleteAsync(id);
            return NoContent();
        }
    }
}
