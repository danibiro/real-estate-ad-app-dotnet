// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bdim1996_dotnet.Controllers.Dto.Incoming;
using bdim1996_dotnet.Controllers.Dto.Outgoing;
using bdim1996_dotnet.Models.Entities;
using bdim1996_dotnet.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bdim1996_dotnet.Controllers
{
    [Route("ads")]
    [ApiController]
    public class RealEstateAdController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly RealEstateContext _context;

        public RealEstateAdController(ILogger<RealEstateAdController> logger, IMapper mapper, RealEstateContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RealEstateAdDetailsDto>>> GetRealEstateAds()
        {
            _logger.LogInformation("GetRealEstateAds() called");
            if (_context.RealEstateAds == null)
            {
                _logger.LogError("No ads found!");
                return NotFound();
            }
            var realEstateAds = await _context.RealEstateAds.ToListAsync();
            realEstateAds.ForEach(a => a.Agent = _context.RealEstateAgents.Find(a.AgentId)!);
            var realEstateAdDetailsDtos = new List<RealEstateAdDetailsDto>();
            foreach (var realEstateAd in realEstateAds)
            {
                var realEstateAdDetailsDto = _mapper.Map<RealEstateAd, RealEstateAdDetailsDto>(realEstateAd);
                realEstateAdDetailsDtos.Add(realEstateAdDetailsDto);
            }
            return Ok(realEstateAdDetailsDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RealEstateAdDetailsDto>> GetRealEstateAd(long id)
        {
            _logger.LogInformation("GetRealEstateAd({id}) called", id);
            if (_context.RealEstateAds == null)
            {
                _logger.LogInformation("No ads found in the database!");
                return NotFound();
            }
            var realEstateAd = await _context.RealEstateAds.FindAsync(id);

            if (realEstateAd == null)
            {
                _logger.LogError("No ad found by the id {id}", id);
                return NotFound();
            }
            var realEstateAdDetailsDto = _mapper.Map<RealEstateAd, RealEstateAdDetailsDto>(realEstateAd);
            return Ok(realEstateAdDetailsDto);
        }

        // PUT: api/RealEstateAd/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRealEstateAd(long id, RealEstateAdCreationDto realEstateAdCreationDto)
        {
            _logger.LogInformation("PutRealEstateAd({id}) called", id);
            var realEstateAd = _mapper.Map<RealEstateAdCreationDto, RealEstateAd>(realEstateAdCreationDto);
            var oldRealEstateAd = _context.RealEstateAds.FirstOrDefault(a => a.Id == id);

            if (oldRealEstateAd == null)
            {
                _logger.LogError("No ad found by the id {id}", id);
                return NotFound("Ad with id " + id.ToString() + " does not exist.");
            }

            if (_context.RealEstateAgents.Find(realEstateAd.AgentId) == null)
            {
                _logger.LogError("No agent found by the id {id}", realEstateAd.AgentId);
                return NotFound("Agent with id " + realEstateAd.AgentId.ToString() + " does not exist.");
            }

            oldRealEstateAd.Description = realEstateAd.Description;
            oldRealEstateAd.Address = realEstateAd.Address;
            oldRealEstateAd.Price = realEstateAd.Price;
            oldRealEstateAd.AgentId = realEstateAd.AgentId;
            oldRealEstateAd.Area = realEstateAd.Area;
            oldRealEstateAd.Negotiable = realEstateAd.Negotiable;
            oldRealEstateAd.DateOfCreation = realEstateAd.DateOfCreation;
            oldRealEstateAd.Title = realEstateAd.Title;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!RealEstateAdExists(id))
            {
                return NotFound();
            }

            return Ok("The updated ad's id is " + oldRealEstateAd.Id);
        }

        // POST: api/RealEstateAd
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostRealEstateAd(RealEstateAdCreationDto realEstateAdCreationDto)
        {
            _logger.LogInformation("PostRealEstateAd() called");
            var realEstateAd = _mapper.Map<RealEstateAdCreationDto, RealEstateAd>(realEstateAdCreationDto);
            if (_context.RealEstateAds == null)
            {
                return Problem("Entity set 'RealEstateAdContext.RealEstateAds'  is null.");
            }
            if (_context.RealEstateAgents.Find(realEstateAd.AgentId) == null)
            {
                return NotFound("Agent with id " + realEstateAd.AgentId.ToString() + " does not exist.");
            }
            _context.RealEstateAds.Add(realEstateAd);
            await _context.SaveChangesAsync();

            return Ok("GetRealEstateAd(" + realEstateAd.Id + ")");
        }

        // DELETE: api/RealEstateAd/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRealEstateAd(long id)
        {
            _logger.LogInformation("DeleteRealEstateAd({id}) called", id);
            if (_context.RealEstateAds == null)
            {
                return NotFound();
            }
            var realEstateAd = await _context.RealEstateAds.FindAsync(id);
            if (realEstateAd == null)
            {
                return NotFound();
            }

            _context.RealEstateAds.Remove(realEstateAd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RealEstateAdExists(long id)
        {
            return (_context.RealEstateAds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
