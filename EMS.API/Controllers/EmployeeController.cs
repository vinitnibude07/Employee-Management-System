using EMS.API.Data;
using EMS.API.Features.Employees.Commands.CreateEmployee;
using EMS.API.Features.Employees.Commands.DeleteEmployee;
using EMS.API.Features.Employees.Commands.UpdateEmployee;
using EMS.API.Features.Employees.Queries.GetAllEmployees;
using EMS.API.Features.Employees.Queries.GetEmployeeById;
using EMS.API.Features.Employees.Queries.GetPagedEmployees;
using EMS.API.Features.Employees.Queries.GetEmployeePdf; 
using EMS.Shared.DTOs;
using EMS.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;


        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeCreateDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateEmployeeCommand
            {
                EmployeeName = request.EmployeeName,
                Description = request.Description,
                Salary = request.Salary,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DepartmentId = request.DepartmentId, 
                IsActive = request.IsActive,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _mediator.Send(
                new GetAllEmployeesQuery());

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _mediator.Send(
                new GetEmployeeByIdQuery
                {
                    Id = id
                });

            if (employee == null)
                return NotFound("Employee not found.");

            return Ok(employee);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeUpdateDto request)
        {
            Console.WriteLine($"DEBUG: API received request for ID: {request.Id}, DepartmentId: {request.DepartmentId}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new UpdateEmployeeCommand
            {
                Id = request.Id,
                EmployeeName = request.EmployeeName,
                Description = request.Description,
                Salary = request.Salary,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DepartmentId = request.DepartmentId,
                IsActive = request.IsActive,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };

            var result = await _mediator.Send(command);

            if (result == "Employee not found.")
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _mediator.Send(
                new DeleteEmployeeCommand
                {
                    Id = id
                });

            if (result == "Employee not found.")
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedEmployees(
            int pageNumber = 1,
            int pageSize = 5,
            string sortBy = "id",
            string searchText = "")
        {
            var query = new GetPagedEmployeesQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortBy = sortBy,
                SearchText = searchText
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        // --- NEW PDF DOWNLOAD ENDPOINT ---
        [HttpGet("download-pdf")]
        public async Task<IActionResult> DownloadPdf()
        {
            var fileBytes = await _mediator.Send(new GetEmployeePdfQuery());

            return File(fileBytes, "application/pdf", "EmployeeList.pdf");
        }
    }
}