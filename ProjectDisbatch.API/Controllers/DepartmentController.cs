using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDisbatch.API.CustomActionFilters;
using ProjectDisbatch.API.Data;
using ProjectDisbatch.API.Models.Domain;
using ProjectDisbatch.API.Models.DTO;
using ProjectDisbatch.API.Repositories;
using System.Text.Json;

namespace ProjectDisbatch.API.Controllers
{
    //https://localhost:portnumber/api/department
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IDepartmentRepository departmentRepository;
        private readonly ILogger<DepartmentController> logger;

        public DepartmentController(IMapper mapper, IDepartmentRepository departmentRepository, ILogger<DepartmentController> logger)
        {
            this.mapper = mapper;
            this.departmentRepository = departmentRepository;
            this.logger = logger;
        }

        //GET ALL DEPARTMENTS
        //GET: https://localhost:portnumber/api/department
        [HttpGet]
        //[Authorize(Roles = "Reader,Writer")] TODO Renable this later
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("GetAll Department Action Method was Invoked");

            //logger.LogWarning("This is a warning");

            //Get Data From Database - Domain Models
            var departmentsDomain = await departmentRepository.GetAllAsync();

            //Map Domain Models to DTOs using automapper
            var departmentsDto = mapper.Map<List<DepartmentDto>>(departmentsDomain);

            logger.LogInformation($"GetAll Department finished with data: {JsonSerializer.Serialize(departmentsDto)}");

            //Return DTOs
            return Ok(departmentsDto);
        }

        //GET SINGLE DEPARTMENT (By ID)
        //GET: https://localhost:portnumber/api/department/{id}
        [HttpGet]
        //[Authorize(Roles = "Reader,Writer")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id); //Find() ONLY TAKES PRIMARY KEY
            var departmentDomain = await departmentRepository.GetByIdAsync(id);
            if (departmentDomain == null)
            {
                return NotFound();
            }

            //Map domain to DTO
            var departmentDto = mapper.Map<DepartmentDto>(departmentDomain);

            return Ok(departmentDto);
        }

        //POST Create New Department
        //POST https://localhost:portnumber/api/department
        [HttpPost]
        //[Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddDepartmentRequestDto addDepartmentRequestDto)
        {
            //Map DTO to Domain Model
            var departmentDomainModel = mapper.Map<Department>(addDepartmentRequestDto);

            //Use Domain Model to create Department
            departmentDomainModel = await departmentRepository.CreateAsync(departmentDomainModel);

            // Map Domain Model back to DTO
            var departmentDto = mapper.Map<DepartmentDto>(departmentDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = departmentDto.Id }, departmentDto);
        }

        //Update Department
        //PUT: https://localhost:portnumber/api/department/{id}
        [HttpPut]
        //[Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDepartmentRequestDto updateDepartmentRequestDto)
        {
            // Map DTO to Domain Model
            var departmentDomainModel = mapper.Map<Department>(updateDepartmentRequestDto);

            //Update Domain Model
            departmentDomainModel = await departmentRepository.UpdateAsync(id, departmentDomainModel);

            //Check if department exists
            if (departmentDomainModel == null)
            {
                return NotFound();
            }

            //Convert Domain Model to DTO
            var departmentDto = mapper.Map<DepartmentDto>(departmentDomainModel);

            return Ok(departmentDto);
        }

        //Delete Department
        //DELETE: https://localhost:portnumber/api/department/{id}
        [HttpDelete]
        //[Authorize(Roles = "Writer")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Check if department exists
            var departmentDomainModel = await departmentRepository.DeleteAsync(id);

            if (departmentDomainModel == null)
            {
                return NotFound();
            }

            //Optional: Return deleted department back
            //Map Domain Model to DTO
            var departmentDto = mapper.Map<DepartmentDto>(departmentDomainModel);

            return Ok(departmentDto);
        }
    }
}
