using SalaryCalculator.SharedKernel;
using System;

namespace SalaryCalculator.Core
{
    internal class DefaultCalculator : Calculator
    {
        public DefaultCalculator(Employee employee) : base(employee)
        {
        }

        public override Salary ComputeSalary(decimal input)
        {
            return Salary.Create(0m).Value;
        }
    }
}