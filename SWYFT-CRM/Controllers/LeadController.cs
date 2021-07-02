using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWYFT_CRM.Models;
using SWYFT_CRM.Repositories;

namespace SWYFT_CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeadController : ControllerBase
    {
        private readonly ILeadRepository _leadRepository;

        public LeadController(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        // GET: api/<LeadController>
        [HttpGet("GetUserLeads/{id}")]
        public IActionResult GetByUser(int id)
        {
            return Ok(_leadRepository.GetAllUserLeads(id));
        }

        // GET api/<LeadController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var lead = _leadRepository.GetById(id);
            if(lead == null)
            {
                return NotFound();
            }
            return Ok(lead);
        }

        // POST api/<LeadController>
        [HttpPost]
        public IActionResult Post(Lead lead)
        {
            _leadRepository.Add(lead);
            return Ok(lead);
        }

        // PUT api/<LeadController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Lead lead)
        {
            _leadRepository.UpdateLead(lead);
            return NoContent();
        }

        // DELETE api/<LeadController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _leadRepository.Delete(id);
            return NoContent();
        }
    }
}
