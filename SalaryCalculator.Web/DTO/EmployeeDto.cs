using SalaryCalculator.Core;
using System;

namespace SalaryCalculator.Web
{
    public abstract class EmployeeDto
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string TIN { get; set; }
        public EmployeeType Type { get; set; }
        public decimal Salary { get; set; }

        public EmployeeDto(Employee employee)
        {
            if (employee == null)
                return;

            Name = employee.Name.Value;
            BirthDate = employee.BirthDate.Value;
            TIN = employee.TIN.Value;
            Salary = employee.Salary;
            Type = employee.EmployeeType;
        }
    }
}