using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPFinal_GSC.DataAccess.Interfaces;
using TPFinal_GSC.Dto;
using TPFinal_GSC.Entities;

namespace TPFinal_GSC.Controllers.WebAPI
{
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public PeopleController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] PersonDto personDto)
        {
            var person = mapper.Map<Person>(personDto);

            if (uow.PersonRepository.ExistsName(person.Name))
                return BadRequest("Ya existe una persona con este nombre");

            uow.PersonRepository.Add(person);
            uow.Complete();

            return Ok(person);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAll()
        {
            return Ok(uow.PersonRepository.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin,User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetById(int id)
        {
            return Ok(uow.PersonRepository.GetById(id));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Remove(int id)
        {
            var deleted = uow.PersonRepository.Delete(id);
            if (!deleted)
                return NotFound();
            uow.Complete();

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, [FromBody] Person person)
        {
            if(id != person.Id)
                return BadRequest();
            if (uow.PersonRepository.GetById(id) is null)
                return NotFound();
            uow.PersonRepository.Update(person);
            uow.Complete();

            return Ok(person);
        }

        
    }
}
