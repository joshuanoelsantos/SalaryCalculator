using SalaryCalculator.SharedKernel;
using System;

namespace SalaryCalculator.Core
{
    public class RegularCalculator : Calculator
    {
        private const int DaysPerMonth = 22;
        private const decimal TaxRate = 0.12m;

        public RegularCalculator(Employee employee) : base(employee)
        {
        }

        public override Salary ComputeSalary(decimal daysAbsent)
        {
            decimal absentDeduction = (_employee.Salary.Value / DaysPerMonth) * daysAbsent;
            decimal taxDeduction = _employee.Salary.Value * TaxRate;

            decimal computedSalary = _employee.Salary.Value - absentDeduction - taxDeduction;

            return computedSalary;
        }
    }
}