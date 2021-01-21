using SalaryCalculator.SharedKernel;
using System;

namespace SalaryCalculator.Core
{
    internal class DefaultCalculator : Calculator
    {
        private const decimal DefaultEmptySalary = 0m;

        public DefaultCalculator(Employee employee) : base(employee)
        {
        }

        public override Salary ComputeSalary(decimal input)
        {
            return DefaultEmptySalary;
        }
    }
}