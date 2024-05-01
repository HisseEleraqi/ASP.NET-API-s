using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task2.DTO;
using Task2.GenericRepo;
using Task2.Models;

namespace Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
      
        GenericRepo<Department> Drepo;

        public DepartmentController(GenericRepo<Department>_Drepo)
        {
           Drepo = _Drepo;   
        }
        
        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = Drepo.GetAll();
            List<DepartmentDTO> departmentsDTO = new List<DepartmentDTO>();

            foreach (var dept in departments)
            {
                departmentsDTO.Add(new DepartmentDTO()
                {
                    Dept_Id = dept.Dept_Id,
                    Dept_Name = dept.Dept_Name,
                    Dept_Location = dept.Dept_Location,
                    Students = dept.Students.Select(s => s.St_Fname).ToList()
                });

            }    
                return Ok(departmentsDTO);  
        }

        [HttpGet("{id}")]

        public IActionResult GetDpartmentById(int id){
        
            var department = Drepo.GetById(id);   

            if (department == null)
            {
                return NotFound();
            }
            else
            {
                DepartmentDTO dept = new DepartmentDTO()
                {
                    Dept_Id = department.Dept_Id,
                    Dept_Name = department.Dept_Name,
                    Dept_Location = department.Dept_Location,
                    Students = department.Students.Select(s => s.St_Fname).ToList()
                };
                return Ok(dept);
            }
        
        
        
        }
           
    }
}

