using SalaryCalculator.SharedKernel;
using System;

namespace SalaryCalculator.Core
{
    public class ContractualCalculator : Calculator
    {
        public ContractualCalculator(Employee employee) : base(employee)
        {
        }

        public override Salary ComputeSalary(decimal daysWorked)
        {
            return Salary.Create(daysWorked * _employee.Salary.Value).Value;
        }
    }
}