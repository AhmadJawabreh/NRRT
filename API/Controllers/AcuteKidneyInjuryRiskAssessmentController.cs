/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using BusinessLogic.AcuteKidneyInjuryRiskAssessment;
using Contracts.V1.AcuteKidneyInjuryRiskAssessment.Filters;
using Contracts.V1.AcuteKidneyInjuryRiskAssessment.Models;
using Contracts.V1.AcuteKidneyInjuryRiskAssessment.Resources;
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
    /// Acute Kidney Injury Risk Assessment
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ReadOnly(true)]
    [Route("v1/api/acutekidneyinjuryriskassessment")]
    public class AcuteKidneyInjuryRiskAssessmentController : BaseController
    {
        public readonly IAcuteKidneyInjuryRiskAssessmentManager _aKIRA;

        /// <summary>
        /// Acute Kidney Injury Risk Assessment
        /// </summary>
        public AcuteKidneyInjuryRiskAssessmentController(IAcuteKidneyInjuryRiskAssessmentManager aKIRA) => _aKIRA = aKIRA;

        /// <summary>
        /// Get filter list of Acute Kidney Injury Risk Assessment.
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
        [ProducesResponseType(typeof(ResourceCollection<AcuteKidneyInjuryRiskAssessmentResource>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllItems([FromQuery] AcuteKidneyInjuryRiskAssessmentFilter filter)
        {
            var chronometer = new Stopwatch();
            chronometer.Start();

            var totalResources = await _aKIRA.CountAsync();
            var resources = await _aKIRA.GetItems(filter);
            return Ok(new ResourceCollection<AcuteKidneyInjuryRiskAssessmentResource>(resources, totalResources, chronometer.ElapsedMilliseconds));
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
        [Route("{id:min(1)}", Name = "GetAcuteKidneyInjuryRiskAssessmentById_v1")]
        [ProducesResponseType(typeof(AcuteKidneyInjuryRiskAssessmentResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
        {
            return Ok(await _aKIRA.GetItemById(id));
        }

        /// <summary>
        /// Create a Acute Kidney Injury Risk Assessment Resource.
        /// </summary>
        /// <remarks>Create a Acute Kidney Injury Risk Assessment Resource.</remarks>
        /// <param name="model"></param>
        /// <response code="201">item is created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPost]
        [Route("", Name = "CreateAcuteKidneyInjuryRiskAssessmentResource_v1")]
        [ProducesResponseType(typeof(AcuteKidneyInjuryRiskAssessmentResource), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateItem([FromBody] AcuteKidneyInjuryRiskAssessmentModel model)
        {
            var resource = await _aKIRA.CreateAsync(model);
            return CreatedAtAction(nameof(GetItemById), new { id = resource.Id }, resource);
        }

        /// <summary>
        /// Update a Acute Kidney Injury Risk Assessment by id.
        /// </summary>
        /// <remarks>Update a Acute Kidney Injury Risk Assessment by id.</remarks>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <response code="200">item is updated.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:min(1)}", Name = "AcuteKidneyInjuryRiskAssessment_v1")]
        [ProducesResponseType(typeof(AcuteKidneyInjuryRiskAssessmentResource), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] AcuteKidneyInjuryRiskAssessmentModel model)
        {
            return Ok(await _aKIRA.UpdateAsync(id, model));
        }

        /// <summary>
        /// Delete Acute Kidney Injury Risk Assessment by id.
        /// </summary>
        /// <remarks>Delete Acute Kidney Injury Risk Assessment by id.</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">No Content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Item is not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="500">Internal Server error.</response>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:min(1)}", Name = "DeleteAcuteKidneyInjuryRiskAssessment_v1")]
        [ProducesResponseType(typeof(AcuteKidneyInjuryRiskAssessmentResource), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _aKIRA.DeleteAsync(id);
            return NoContent();
        }
    }
}
