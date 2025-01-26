using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDisbatch.API.CustomActionFilters;
using ProjectDisbatch.API.Models.Domain;
using ProjectDisbatch.API.Models.DTO;
using ProjectDisbatch.API.Repositories;

namespace ProjectDisbatch.API.Controllers
{
    // /api/project
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProjectRepository projectRepository;

        public ProjectController(IMapper mapper, IProjectRepository projectRepository)
        {
            this.mapper = mapper;
            this.projectRepository = projectRepository;
        }

        //CREATE Project
        //POST: /api/project
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddProjectRequestDto projectRequestDto)
        {
            //Map DTO to Domain Model
            var projectDomain = mapper.Map<Project>(projectRequestDto);

            //Save Domain Model in Database
            projectDomain = await projectRepository.CreateAsync(projectDomain);

            //Map Domain Model to DTO
            var projectDto = mapper.Map<ProjectDto>(projectDomain);

            return CreatedAtAction(nameof(GetById), new { id = projectDto.Id }, projectDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Domain Models from Database
            var projectsDomain = await projectRepository.GetAllAsync();

            //Map Domain Model to Dto
            var projectDto = mapper.Map<List<ProjectDto>>(projectsDomain);

            //Return Dto
            return Ok(projectDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Domain Model From Database
            var projectDomain = await projectRepository.GetByIdAsync(id);

            //Check if exists
            if (projectDomain == null)
            {
                return NotFound();
            }

            //Map to Dto
            var projectDto = mapper.Map<ProjectDto>(projectDomain);

            return Ok(projectDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProjectRequestDto updateProjectRequestDto)
        {
            //Map DTO to Domain Model
            var projectDomainModel = mapper.Map<Project>(updateProjectRequestDto);

            //Update domain Model
            projectDomainModel = await projectRepository.UpdateAsync(id, projectDomainModel);

            //Check if exists
            if(projectDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            var projectDto = mapper.Map<ProjectDto>(projectDomainModel);

            //Return DTO
            return Ok(projectDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Delete
            var projectDomainModel = await projectRepository.DeleteAsync(id);

            //Check if exists
            if (projectDomainModel == null)
            {
                return NotFound();
            }

            //Return deleted DTO back
            var projectDto = mapper.Map<ProjectDto>(projectDomainModel);
            return Ok(projectDto);
        }
    }
}
