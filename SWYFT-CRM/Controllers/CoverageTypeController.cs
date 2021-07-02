using Microsoft.AspNetCore.Mvc;
using SWYFT_CRM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWYFT_CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoverageTypeController : ControllerBase
    {
        private readonly ICoverageTypeRepository _coverageTypeRepository;
        public CoverageTypeController(ICoverageTypeRepository coverageTypeRepository)
        {
            _coverageTypeRepository = coverageTypeRepository;
        }

        // GET: api/<CoverageTypeController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_coverageTypeRepository.GetAll());
        }

        // GET api/<CoverageTypeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CoverageTypeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CoverageTypeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CoverageTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
