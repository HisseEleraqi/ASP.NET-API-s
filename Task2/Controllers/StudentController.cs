using Google.Apis.Admin.Directory.directory_v1.Data;
using ICSharpCode.Decompiler.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Task2.DTO;
using Task2.GenericRepo;
using Task2.IRepo;
using Task2.Models;
using Task2.UnitRepo;

namespace Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        UnitOfWork Urepo;
        public StudentController(UnitOfWork repo)
        {
            Urepo = repo;
        }
        [HttpGet]
        [Authorize]
  
        public ActionResult GetAllStudent()
        {
            var std = Urepo.StudentRepo.GetAll();
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

        [HttpGet("{id}")]

        public ActionResult GetStudentById(int id)
        {
            var student = Urepo.StudentRepo.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                StudentDTO std = new StudentDTO()
                {
                    St_Fname = student.St_Fname,
                    St_Lname = student.St_Lname,
                    St_Id = student.St_Id,
                    St_Address = student.St_Address,
                    Age = student.St_Age,
                    Subervisor = student.St_superNavigation.St_Fname,
                    DepartmentName = student.Dept.Dept_Name,

                };
                return Ok(std);

            }

        }
        [HttpPost]

        public ActionResult AddStudent(StudentDTO Std)
        {

            if (Std == null)
            {
                return BadRequest();
            }
            else
            {
                Student s = new Student()
                {
                    St_Fname = Std.St_Fname,
                    St_Lname = Std.St_Lname,
                    St_Address = Std.St_Address,
                    St_Age = Std.Age,


                };
                Urepo.StudentRepo.Add(s);
                Urepo.StudentRepo.Save();
                return Created("Student Added", s);

            }



        }
        [HttpGet("Pagenation")]

        public ActionResult GetStudentByPagenation(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var students = Urepo.StudentRepo.GetByPage(page, pageSize);
            List<StudentDTO> studentsDTO = new List<StudentDTO>();
            foreach (var student in students)
            {
                StudentDTO std = new StudentDTO()
                {
                    St_Fname = student.St_Fname,
                    St_Lname = student.St_Lname,
                    St_Id = student.St_Id,
                    St_Address = student.St_Address,
                    Age = student.St_Age,
                    Subervisor = student.St_superNavigation?.St_Fname ?? "No SuperVisor",
                    DepartmentName = student.Dept?.Dept_Name ?? "No Department",

                };
                studentsDTO.Add(std);
            }
            return Ok(studentsDTO);
        }

        [HttpGet("Search")]

        public ActionResult SearchStudent(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }

            var students = Urepo.StudentRepo.Search<Student>(nameof(Student.St_Fname), name);

            List<StudentDTO> studentsDTO = new List<StudentDTO>();
            foreach (var student in students)
            {
                StudentDTO std = new StudentDTO()
                {
                    St_Fname = student.St_Fname,
                    St_Lname = student.St_Lname,
                    St_Id = student.St_Id,
                    St_Address = student.St_Address,
                    Age = student.St_Age,
                    Subervisor = student.St_superNavigation?.St_Fname ?? "No SuperVisor",
                    DepartmentName = student.Dept?.Dept_Name ?? "No Department",
                };
                studentsDTO.Add(std);
            }
            return Ok(studentsDTO);
        }






    }
}
