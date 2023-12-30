/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.PatientHistory;
using Contracts.V1.PatientHistory.Filters;
using Contracts.V1.PatientHistory.Models;
using Contracts.V1.PatientHistory.Resources;
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
    /// Patients History
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ReadOnly(true)]
    [Route("v1/api/patientshistory")]
    public class PatientHistoryController : BaseController
    {
        public readonly IPatientHistoryManager _patientHistoryManager;

        /// <summary>
        /// Patients History
        /// </summary>
        public PatientHistoryController(IPatientHistoryManager patientHistoryManager) => _patientHistoryManager = patientHistoryManager;

        /// <summary>
        /// Get filter list of Patient History.
        /// </summary>
        /// <remarks>Get filter list of Patient History.</remarks>
        /// <param name="filter"></param>
        /// <response code="200">item are returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResourceCollection<PatientHistoryResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllItems([FromQuery] PatientHistoryListFilter filter)
        {
            var chronometer = new Stopwatch();
            chronometer.Start();

            var totalResources = await _patientHistoryManager.CountAsync();
            var resources = await _patientHistoryManager.GetItems(filter);
            return Ok(new ResourceCollection<PatientHistoryResource>(resources, totalResources, chronometer.ElapsedMilliseconds));
        }

        /// <summary>
        ///  Get a Patient History by id.
        /// </summary>
        /// <remarks>Get a Patient History by id.</remarks>
        /// <param name="id"></param>
        /// <response code="200">item is returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:min(1)}", Name = "GetPatientHistoryById_v1")]
        [ProducesResponseType(typeof(PatientHistoryResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
        {
            return Ok(await _patientHistoryManager.GetItemById(id));
        }

        /// <summary>
        /// Create a Patient History.
        /// </summary>
        /// <remarks>Create a Patient History.</remarks>
        /// <param name="model"></param>
        /// <response code="201">item is created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreatePatientHistory_v1")]
        [ProducesResponseType(typeof(PatientHistoryResource), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateItem([FromBody] PatientHistoryModel model)
        {
            var resource = await _patientHistoryManager.CreateAsync(model);
            return CreatedAtAction(nameof(GetItemById), new { id = resource.Id }, resource);
        }

        /// <summary>
        /// Update a Patient History by id.
        /// </summary>
        /// <remarks>Update a Patient History by Id.</remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">item is updated.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:min(1)}", Name = "UpdatePatientHistory_v1")]
        [ProducesResponseType(typeof(PatientHistoryResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] PatientHistoryModel model)
        {
            return Ok(await _patientHistoryManager.UpdateAsync(id, model));
        }

        /// <summary>
        /// Delete a Patient History by id.
        /// </summary>
        /// <remarks>Delete a Patient History by id.</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">No Content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:min(1)}", Name = "DeletePatientHistory_v1")]
        [ProducesResponseType(typeof(PatientHistoryResource), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _patientHistoryManager.DeleteAsync(id);
            return NoContent();
        }
    }
}
