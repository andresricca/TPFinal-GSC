using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TPFinal_GSC.Controllers.WebAPI;
using TPFinal_GSC.DataAccess.Interfaces;
using TPFinal_GSC.Dto;
using TPFinal_GSC.Entities;

namespace Tests
{
    public class PeopleControllerTest
    {
        private PeopleController target;
        private Mock<IUnitOfWork> mockUow;
        private Mock<IMapper> mockMapper;

        private PersonDto personDto;
        private Person person;
        private int id;

        public PeopleControllerTest()
        {
            mockUow = new Mock<IUnitOfWork>();
            mockMapper = new Mock<IMapper>();

            target = new PeopleController(mockUow.Object, mockMapper.Object);

            personDto = new PersonDto();
            id = 1;
            person = new Person()
            {
                Id = 1,
                Name = "Test"
            };
            mockMapper.Setup(m => m.Map<Person>(personDto)).Returns(person);
            mockUow.Setup(x => x.PersonRepository.ExistsName("Test")).Returns(false);
            mockUow.Setup(x => x.PersonRepository.Delete(id)).Returns(true);
            mockUow.Setup(x => x.PersonRepository.GetById(id)).Returns(person);
        }


        [Fact]
        public void Create_return_bad_request_if_name_exists()
        {
            mockUow.Setup(x => x.PersonRepository.ExistsName("Test")).Returns(true);

            var result = target.Create(personDto) as ObjectResult;

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public void Create_calls_add_and_complete()
        {
            target.Create(personDto);

            mockUow.Verify(u => u.PersonRepository.Add(person), Times.Once());
            mockUow.Verify(u => u.Complete(), Times.Once());
        }

        [Fact]
        public void Create_returns_ok_whit_person()
        {
            var result = target.Create(personDto) as ObjectResult;

            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(person);
        }


        [Fact]
        public void GetAll_returns_list_of_people()
        {
            var peopleList = new List<Person>();
            mockUow.Setup(x => x.PersonRepository.GetAll()).Returns(peopleList);

            var result = target.GetAll() as ObjectResult;

            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(peopleList);
        }


        [Fact]
        public void GetById_returns_person()
        {
            var result = target.GetById(id) as ObjectResult;

            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(person);
        }


        [Fact]
        public void Remove_returns_not_found_if_id_not_exist()
        {
            mockUow.Setup(x => x.PersonRepository.Delete(id)).Returns(false);

            var result = target.Remove(id);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Remove_calls_delete_and_complete()
        {
            target.Remove(id);

            mockUow.Verify(u => u.PersonRepository.Delete(id), Times.Once());
            mockUow.Verify(u => u.Complete(), Times.Once());
        }

        [Fact]
        public void Remove_returns_no_content()
        {
            var result = target.Remove(id);

            result.Should().BeOfType<NoContentResult>();
        }


        [Fact]
        public void Update_returns_bad_request_if_id_does_not_match()
        {
            var id = 2;

            var result = target.Update(id, person);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void Update_returns_not_found_if_id_not_exist()
        {
            mockUow.Setup(x => x.PersonRepository.GetById(id)).Returns((Person)null);

            var result = target.Update(id, person);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Update_calls_update_and_complete()
        {
            target.Update(id, person);

            mockUow.Verify(u => u.PersonRepository.Update(person), Times.Once());
            mockUow.Verify(u => u.Complete(), Times.Once());
        }

        [Fact]
        public void Update_returns_person()
        {
            var result = target.Update(id, person) as ObjectResult;

            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(person);
        }
    }
}
