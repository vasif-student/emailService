﻿using Api.Pagination;
using Api.Tools.EmailHandler;
using Api.Tools.EmailHandler.Abstraction;
using AutoMapper;
using DomainModels.Models;
using DomainModels.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IRepository<Student> _repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _mailservice;

        public StudentsController(IRepository<Student> repository, IMapper mapper, IEmailService mailservice)
        {
            _repository = repository;
            _mapper = mapper;
            _mailservice = mailservice;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllStudents(int pageNumber, int itemCount)
        {
            var students =  _repository.GetAllAsyncAsNoTracking();

            PaginationDto<Student> pagination = new PaginationDto<Student>(students, pageNumber, itemCount);    

            return Ok(pagination);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var student = await _repository.GetAsync(id);
            if (student == null) return NotFound("Bu idde student yoxdur");
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            var result = await _repository.AddAsync(student);
            if (!result) return BadRequest("Something bad happened");
            await _mailservice.SendEmailAsync(new MailRequest { ToEmail = "vasifah@code.edu.az", Subject = "Congrats", Body = $"{student.Name} {student.Surname} successfully created"})
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StudentDto studentDto)
        {
            Student existStudent = await _repository.GetAsync(id);
            if (existStudent == null) return NotFound("There is no student with this id");
            existStudent.Name = studentDto.Name;
            existStudent.Surname = studentDto.Surname;
            bool result = _repository.Update(existStudent);
            if (!result) return BadRequest("Something bad happened");
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var student = await _repository.GetAsync(id);
            if (!await _repository.DeleteAsync(student)) return BadRequest("Something bad happened");
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
