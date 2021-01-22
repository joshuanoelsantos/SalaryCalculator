using SalaryCalculator.SharedKernel;
using System;

namespace SalaryCalculator.Core
{
    public class ContractualCalculator : Calculator
    {
        public ContractualCalculator(Employee employee) : base(employee)
        {
        }

        public override decimal ComputeSalary(decimal daysWorked)
        {
            decimal computedSalary = daysWorked * _employee.Salary.Value;
            return computedSalary.Round();
        }
    }
}