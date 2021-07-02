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
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        // GET: api/<AppointmentController>
        [HttpGet("GetUserAppointments/{id}")]
        public IActionResult GetUserAppointments(int id)
        {
            return Ok(_appointmentRepository.GetAllUserAppointments(id));
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var appointment = _appointmentRepository.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        // POST api/<AppointmentController>
        [HttpPost]
        public IActionResult Post(Appointment appointment)
        {
            _appointmentRepository.Add(appointment);
            return Ok(appointment);
        }

        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Appointment appointment)
        {
            _appointmentRepository.Update(appointment);
            return NoContent();
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _appointmentRepository.Delete(id);
            return NoContent();
        }
    }
}
