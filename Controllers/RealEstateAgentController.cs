// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bdim1996_dotnet;
using bdim1996_dotnet.Controllers.Dto.Incoming;
using bdim1996_dotnet.Controllers.Dto.Outgoing;
using bdim1996_dotnet.Models.Entities;
using bdim1996_dotnet.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bdim1996_dotnet.Controllers
{
    [Route("agents")]
    [ApiController]
    public class RealEstateAgentController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly RealEstateContext _context;

        public RealEstateAgentController(ILogger<RealEstateAgentController> logger, IMapper mapper, RealEstateContext adContext)
        {
            _logger = logger;
            _mapper = mapper;
            _context = adContext;
        }

        // GET: api/RealEstateAgent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RealEstateAgentDetailsDto>>> GetRealEstateAgents()
        {
            _logger.LogInformation("GetRealEstateAgents() called");
            if (_context.RealEstateAgents == null)
            {
                _logger.LogError("No agents found!");
                return NotFound();
            }
            var realEstateAgents = await _context.RealEstateAgents.ToListAsync();
            var realEstateAgentDetailsDtos = new List<RealEstateAgentDetailsDto>();
            foreach (var realEstateAgent in realEstateAgents)
            {
                var realEstateAgentDetailsDto = _mapper.Map<RealEstateAgent, RealEstateAgentDetailsDto>(realEstateAgent);
                realEstateAgentDetailsDtos.Add(realEstateAgentDetailsDto);
            }
            return Ok(realEstateAgentDetailsDtos);
        }

        // GET: api/RealEstateAgent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RealEstateAgentDetailsDto>> GetRealEstateAgent(long id)
        {
            _logger.LogInformation("GetRealEstateAgent({id}) called", id);
            if (_context.RealEstateAgents == null)
            {
                _logger.LogError("No agents found in the database!");
                return NotFound();
            }
            var realEstateAgent = await _context.RealEstateAgents.FindAsync(id);

            if (realEstateAgent == null)
            {
                _logger.LogError("No agent with id {id} found!", id);
                return NotFound();
            }

            var realEstateAgentDetailsDto = _mapper.Map<RealEstateAgent, RealEstateAgentDetailsDto>(realEstateAgent);
            return Ok(realEstateAgentDetailsDto);
        }

        // PUT: api/RealEstateAgent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRealEstateAgent(long id, RealEstateAgentCreationDto realEstateAgentCreationDto)
        {
            _logger.LogInformation("PutRealEstateAgent({id}) called", id);
            var realEstateAgent = _mapper.Map<RealEstateAgentCreationDto, RealEstateAgent>(realEstateAgentCreationDto);
            var oldRealEstateAgent = _context.RealEstateAgents.FirstOrDefault(agent => agent.Id == id);
            if (oldRealEstateAgent == null)
            {
                _logger.LogError("No agent with id {id} found!", id);
                return NotFound();
            }

            oldRealEstateAgent.Name = realEstateAgent.Name;
            oldRealEstateAgent.Email = realEstateAgent.Email;
            oldRealEstateAgent.Phone = realEstateAgent.Phone;
            oldRealEstateAgent.Address = realEstateAgent.Address;
            oldRealEstateAgent.Age = realEstateAgent.Age;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!RealEstateAgentExists(id))
            {
                return NotFound();
            }

            return Ok("The updated agent's id is " + id + ".");
        }

        // POST: api/RealEstateAgent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostRealEstateAgent(RealEstateAgentCreationDto realEstateAgentCreationDto)
        {
            _logger.LogInformation("PostRealEstateAgent() called");
            var realEstateAgent = _mapper.Map<RealEstateAgentCreationDto, RealEstateAgent>(realEstateAgentCreationDto);
            if (_context.RealEstateAgents == null)
            {
                return Problem("Entity set 'RealEstateAgentContext.RealEstateAgents'  is null.");
            }
            _context.RealEstateAgents.Add(realEstateAgent);
            await _context.SaveChangesAsync();

            return Ok("The new Agent's id is: " + realEstateAgent.Id);
        }

        // DELETE: api/RealEstateAgent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRealEstateAgent(long id)
        {
            _logger.LogInformation("DeleteRealEstateAgent({id}) called", id);
            if (_context.RealEstateAgents == null)
            {
                return NotFound();
            }
            var realEstateAgent = await _context.RealEstateAgents.FindAsync(id);
            if (realEstateAgent == null)
            {
                return NotFound();
            }

            var realEstateAdsByAgent = _context.RealEstateAds.ToList().FindAll(ad => ad.AgentId == id);
            foreach (var realEstateAd in realEstateAdsByAgent)
            {
                _logger.LogInformation("Deleting ad with id {id}", realEstateAd.Id);
                _context.RealEstateAds.Remove(realEstateAd);
            }

            await _context.SaveChangesAsync();
            _context.RealEstateAgents.Remove(realEstateAgent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RealEstateAgentExists(long id)
        {
            return (_context.RealEstateAgents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
