using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SalaryCalculator.Core;
using SalaryCalculator.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryCalculator.Web.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeReadDto>>> GetAll()
        {
            return (await _service.GetAll())
                .Select(x => new EmployeeReadDto(x))
                .ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> Get(string id)
        {
            Guid employeeId;

            if (!Guid.TryParse(id, out employeeId))
                return BadRequest("Employee Id is not valid");

            var employee = await _service.Find(employeeId);

            if (employee == null)
                return NotFound();

            return new EmployeeReadDto(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeReadDto>> Create([FromBody] EmployeeCreateDto dto)
        {
            var nameOrError = Name.Create(dto.Name);
            var birthDateOrError = BirthDate.Create(dto.BirthDate);
            var tinOrError = TIN.Create(dto.TIN);
            var salaryOrError = Salary.Create(dto.Salary);

            var result = Result.Combine(
                nameOrError,
                birthDateOrError,
                tinOrError,
                salaryOrError);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var employee = new Employee()
            {
                Name = nameOrError.Value,
                BirthDate = birthDateOrError.Value,
                TIN = tinOrError.Value,
                Salary = salaryOrError.Value,
                EmployeeType = dto.Type
            };

            return new EmployeeReadDto(await _service.Create(employee));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> Update(string id, [FromBody] EmployeeCreateDto dto)
        {
            Guid employeeId;

            if (!Guid.TryParse(id, out employeeId))
                return BadRequest("Employee Id is not valid");

            var employee = await _service.Find(employeeId);

            if (employee == null)
                return NotFound();

            var nameOrError = Name.Create(dto.Name);
            var birthDateOrError = BirthDate.Create(dto.BirthDate);
            var tinOrError = TIN.Create(dto.TIN);
            var salaryOrError = Salary.Create(dto.Salary);

            var result = Result.Combine(
                nameOrError,
                birthDateOrError,
                tinOrError,
                salaryOrError);

            if (result.IsFailure)
                return BadRequest(result.Error);

            employee.Name = nameOrError.Value;
            employee.BirthDate = birthDateOrError.Value;
            employee.TIN = tinOrError.Value;
            employee.Salary = salaryOrError.Value;
            employee.EmployeeType = dto.Type;

            return new EmployeeReadDto(await _service.Update(employee));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> Delete(string id)
        {
            Guid employeeId;

            if (!Guid.TryParse(id, out employeeId))
            {
                return BadRequest("Employee Id is not valid");
            }

            var employee = await _service.Find(employeeId);

            if (employee == null)
                return NotFound();

            await _service.Delete(employeeId);

            return employeeId;
        }

        [HttpPut("{id}/calculate")]
        public async Task<ActionResult<decimal>> CalculateSalary(string id, [FromBody] decimal input)
        {
            Guid employeeId;

            if (!Guid.TryParse(id, out employeeId))
            {
                return BadRequest("Employee Id is not valid");
            }

            var employee = await _service.Find(employeeId);

            if (employee == null)
                return NotFound();

            var salary = await _service.CalculateSalary(employee, input);

            return salary;
        }

        public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object error)
        {
            return base.BadRequest(new
            {
                Error = error
            });
        }
    }
}