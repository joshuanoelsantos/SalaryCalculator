using SalaryCalculator.SharedKernel;

namespace SalaryCalculator.Core
{
    public class Employee
    {
        public Name Name { get; set; }
        public BirthDate BirthDate { get; set; }
        public TIN TIN { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}