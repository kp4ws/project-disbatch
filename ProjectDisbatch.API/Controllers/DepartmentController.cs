using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDisbatch.API.Data;
using ProjectDisbatch.API.Models.Domain;

namespace ProjectDisbatch.API.Controllers
{
    //https://localhost:portnumber/api/department
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ProjectDisbatchDbContext dbContext;

        public DepartmentController(ProjectDisbatchDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET ALL DEPARTMENTS
        //https://localhost:portnumber/api/department
        [HttpGet]
        public IActionResult GetAll()
        {
            //var departments = new List<Department>
            //{
            //    new Department
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Canada Dept",
            //        Code = "CAD",
            //        Description = "Department responsible for managing Canadian projects."
            //    }
            //};

            var departments = dbContext.Departments.ToList();
            return Ok(departments);
        }

    }
}
