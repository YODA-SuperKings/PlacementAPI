using Microsoft.AspNetCore.Mvc;
using PlacementAPI.Models;
using PlacementAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDetailService _studentDetailService;

        public StudentController(StudentDetailService studentDetailsService) =>
        _studentDetailService = studentDetailsService;

        [HttpGet]
        [Route("GetStudents")]
        public IActionResult Get()
        {
            return Ok(_studentDetailService.Get());
        }

        [HttpGet]
        [Route("GetStudentsByCompany")]
        public IActionResult GetStudentsByCompany(string loggedInEmailId)
        {
            return Ok(_studentDetailService.GetStudentsByCompany(loggedInEmailId));
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<StudentDetails>> Get(string id)
        {
            var studentDetails = await _studentDetailService.GetAsync(id);

            if (studentDetails is null)
            {
                return NotFound();
            }

            return Ok(studentDetails);
        }

        [HttpPost]
        [Route("CreateStudent")]
        public IActionResult Post(StudentDetails newStudentDetails)
        {
            string msg;
            msg = _studentDetailService.CreateStudent(newStudentDetails);
            return Ok(msg);
        }

        [HttpPut("{id:length(24)}")]
        [Route("UpdateStudent")]
        public async Task<IActionResult> Update(IEnumerable<StudentDetails> updatedStudentDetails)
        {
            var id = updatedStudentDetails.FirstOrDefault().Id;
            var studentDetails = await _studentDetailService.GetAsync(id);

            if (studentDetails is null)
            {
                return NotFound();
            }

            await _studentDetailService.UpdateAsync(id, updatedStudentDetails.FirstOrDefault());

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Route("DeleteStudent")]
        public async Task<IActionResult> Delete(string id)
        {
            var studentDetails = await _studentDetailService.GetAsync(id);

            if (studentDetails is null)
            {
                return NotFound();
            }

            await _studentDetailService.RemoveAsync(id);

            return NoContent();
        }
    }
}
