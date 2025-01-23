using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDisbatch.API.Data;
using ProjectDisbatch.API.Models.Domain;
using ProjectDisbatch.API.Models.DTO;

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
        //GET: https://localhost:portnumber/api/department
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

            //Get Data From Database - Domain Models
            var departmentsDomain = dbContext.Departments.ToList();

            //Map Domain Models to DTOs
            var departmentsDto = new List<DepartmentDto>();
            foreach (var departmentDomain in departmentsDomain)
            {
                departmentsDto.Add(new DepartmentDto
                {
                    Id = departmentDomain.Id,
                    Name = departmentDomain.Name,
                    Code = departmentDomain.Code,
                    Description = departmentDomain.Description
                });
            }

            //Return DTOs
            return Ok(departmentsDto);
        }

        //GET SINGLE DEPARTMENT (By ID)
        //GET: https://localhost:portnumber/api/department/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id); //Find() ONLY TAKES PRIMARY KEY
            var departmentDomain = dbContext.Departments.FirstOrDefault(x => x.Id == id);
            if (departmentDomain == null)
            {
                return NotFound();
            }

            //Map domain to DTO
            var departmentDto = new DepartmentDto
            {
                Id = departmentDomain.Id,
                Name = departmentDomain.Name,
                Code = departmentDomain.Code,
                Description = departmentDomain.Description
            };

            return Ok(departmentDto);
        }

        //POST Create New Department
        //POST https://localhost:portnumber/api/department
        [HttpPost]
        public IActionResult Create([FromBody] AddDepartmentRequestDto addDepartmentRequestDto)
        {
            //Map DTO to Domain Model
            var departmentDomainModel = new Department
            {
                Code = addDepartmentRequestDto.Code,
                Name = addDepartmentRequestDto.Name,
                Description = addDepartmentRequestDto.Description
            };

            //Use Domain Model to create Department
            dbContext.Departments.Add(departmentDomainModel);
            dbContext.SaveChanges();

            // Map Domain Model back to DTO
            var departmentDto = new DepartmentDto
            {
                Id = departmentDomainModel.Id,
                Name = departmentDomainModel.Name,
                Code = departmentDomainModel.Code,
                Description = departmentDomainModel.Description
            };

            return CreatedAtAction(nameof(GetById), new { id = departmentDto.Id }, departmentDto);
        }

        //Update Department
        //PUT: https://localhost:portnumber/api/department/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateDepartmentRequestDto updateDepartmentRequestDto)
        {
            //Check if department exists
            var departmentDomainModel = dbContext.Departments.FirstOrDefault(x => x.Id == id);

            if (departmentDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model
            departmentDomainModel.Code = updateDepartmentRequestDto.Code;
            departmentDomainModel.Name = updateDepartmentRequestDto.Name;
            departmentDomainModel.Description = updateDepartmentRequestDto.Description;

            dbContext.SaveChanges();

            //Convert Domain Model to DTO
            var departmentDto = new DepartmentDto
            {
                Name = departmentDomainModel.Name,
                Code = departmentDomainModel.Code,
                Description = departmentDomainModel.Description
            };

            return Ok(departmentDto);
        }

        //Delete Department
        //DELETE: https://localhost:portnumber/api/department/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            //Check if department exists
            var departmentDomainModel = dbContext.Departments.FirstOrDefault(x => x.Id == id);
            if (departmentDomainModel == null)
            {
                return NotFound();
            }

            //Delete department
            dbContext.Remove(departmentDomainModel);
            dbContext.SaveChanges();

            //Optional: Return deleted department back
            //Map Domain Model to DTO
            var departmentDto = new DepartmentDto
            {
                Name = departmentDomainModel.Name,
                Code = departmentDomainModel.Code,
                Description = departmentDomainModel.Description
            };

            return Ok(departmentDto); 
        }
    }
}
