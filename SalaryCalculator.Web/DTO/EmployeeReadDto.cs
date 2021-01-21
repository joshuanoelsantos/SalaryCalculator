using SalaryCalculator.Core;
using System;

namespace SalaryCalculator.Web
{
    public class EmployeeReadDto : EmployeeDto
    {
        public Guid Id { get; set; }

        public EmployeeReadDto() : base(null)
        {
        }

        public EmployeeReadDto(Employee employee) : base(employee)
        {
            Id = employee.Id;
        }
    }
}