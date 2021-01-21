using SalaryCalculator.Core;
using System;

namespace SalaryCalculator.Web
{
    public class EmployeeCreateDto : EmployeeDto
    {
        public EmployeeCreateDto() : base(null)
        {
        }

        public EmployeeCreateDto(Employee employee) : base(employee)
        {
        }
    }
}