using SalaryCalculator.SharedKernel;
using System;

namespace SalaryCalculator.Core
{
    public class Employee
    {
        public Guid Id { get; }
        public Name Name { get; set; }
        public BirthDate BirthDate { get; set; }
        public TIN TIN { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public Salary Salary { get; set; }

        public Employee()
        {
            Id = Guid.NewGuid();
        }
    }
}