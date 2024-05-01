using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.DTO;
using Task2.Models;

namespace Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        ITIContext _db;

        public CourseController(ITIContext db)
        {
            _db = db;
        }
        [Authorize]
        [HttpGet]
      
        public IActionResult GetAll()
        {
            var std = _db.Students.ToList();
            List<StudentDTO> studentsDTO = new List<StudentDTO>();

            foreach (var student in std)
            {
                StudentDTO stdDTO = new StudentDTO()
                {
                    St_Fname = student.St_Fname,
                    St_Lname = student.St_Lname,
                    St_Id = student.St_Id,
                    St_Address = student.St_Address,
                    Age = student.St_Age,
                    Subervisor = student.St_superNavigation?.St_Fname ?? "No SuperVisor",
                    DepartmentName = student.Dept?.Dept_Name ?? "No Department",

                };

                studentsDTO.Add(stdDTO);

            }
            return Ok(studentsDTO);
        }
    }
}
