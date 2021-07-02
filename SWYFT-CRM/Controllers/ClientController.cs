using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWYFT_CRM.Models;
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
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: api/<ClientController>
        [HttpGet("GetUserClients/{id}")]
        public IActionResult GetByUser(int id)
        {
            return Ok(_clientRepository.GetAllUserClients(id));
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var client = _clientRepository.GetById(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        // POST api/<ClientController>
        [HttpPost]
        public IActionResult Post(Lead client)
        {
            _clientRepository.Add(client);
            return Ok(client);
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Lead client)
        {
            _clientRepository.UpdateLead(client);
            return NoContent();
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _clientRepository.Delete(id);
            return NoContent();
        }
    }
}
