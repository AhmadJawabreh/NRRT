/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.Patient;
using Contracts.V1.Patient.Filters;
using Contracts.V1.Patient.Models;
using Contracts.V1.Patient.Resources;
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
    /// Patients
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ReadOnly(true)]
    [Route("v1/api/patients")]
    public class PatientController : BaseController
    {
        public readonly IPatientManager _patientManager;

        /// <summary>
        /// Patients
        /// </summary>
        public PatientController(IPatientManager patientManager) => _patientManager = patientManager;

        /// <summary>
        /// Get filter list of patients.
        /// </summary>
        /// <remarks>Get filter list of patients.</remarks>
        /// <param name="filter"></param>
        /// <response code="200">item are returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResourceCollection<PatientResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllItems([FromQuery] PatientFilter filter)
        {
            var chronometer = new Stopwatch();
            chronometer.Start();

            var totalResources = await _patientManager.CountAsync();
            var resources = await _patientManager.GetItems(filter);
            return Ok(new ResourceCollection<PatientResource>(resources, totalResources, chronometer.ElapsedMilliseconds));
        }

        /// <summary>
        ///  Get a Patient by id.
        /// </summary>
        /// <remarks>Get a Patient by id.</remarks>
        /// <param name="id"></param>
        /// <response code="200">item is returned.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:min(1)}", Name = "GetPatientById_v1")]
        [ProducesResponseType(typeof(PatientResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
        {
            return Ok(await _patientManager.GetItemById(id));
        }

        /// <summary>
        /// Create a Patient.
        /// </summary>
        /// <remarks>Create a Patient.</remarks>
        /// <param name="model"></param>
        /// <response code="201">item is created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreatePatient_v1")]
        [ProducesResponseType(typeof(PatientResource), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateItem([FromBody] PatientModel model)
        {
            var resource = await _patientManager.CreateAsync(model);
            return CreatedAtAction(nameof(GetItemById), new { id = resource.Id }, resource);
        }

        /// <summary>
        /// Update a Patient by id.
        /// </summary>
        /// <remarks>Update a Patient by Id.</remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">item is updated.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:min(1)}", Name = "UpdatePatient_v1")]
        [ProducesResponseType(typeof(PatientResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] PatientModel model)
        {
            return Ok(await _patientManager.UpdateAsync(id, model));
        }

        /// <summary>
        /// Delete a Patient by id.
        /// </summary>
        /// <remarks>Delete a Patient by id.</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">No Content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:min(1)}", Name = "DeletePatient_v1")]
        [ProducesResponseType(typeof(PatientResource), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _patientManager.DeleteAsync(id);
            return NoContent();
        }
    }
}
