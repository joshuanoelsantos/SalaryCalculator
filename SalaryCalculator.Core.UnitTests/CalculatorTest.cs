using FluentAssertions;
using SalaryCalculator.SharedKernel;
using System;
using Xunit;

namespace SalaryCalculator.Core.UnitTests
{
    public class CalculatorTest
    {
        private Employee _employee;

        public CalculatorTest()
        {
            _employee = new Employee()
            {
                Name = Name.Create("Joshua Santos").Value,
                BirthDate = BirthDate.Create(new DateTime(1994, 2, 8)).Value,
                TIN = TIN.Create("123456789").Value
            };
        }

        [Theory]
        [InlineData(1, 16_690.91)]
        [InlineData(0, 17_600)]
        public void WhenCalculateSalaryOfRegularEmployee_CalculateCorrectly(
            decimal absent,
            decimal expectedSalary)
        {
            _employee.EmployeeType = EmployeeType.Regular;
            _employee.Salary = Salary.Create(20_000).Value;

            Calculator calculator = new CalculatorFactory().Create(_employee);

            Salary salary = calculator.ComputeSalary(absent);

            salary.Value.Should().Be(expectedSalary);
        }

        [Theory]
        [InlineData(15.5, 7_750)]
        [InlineData(22, 11_000)]
        public void WhenCalculateSalaryOfContractualEmployee_CalculateCorrectly(
            decimal absent,
            decimal expectedSalary)
        {
            _employee.EmployeeType = EmployeeType.Contractual;
            _employee.Salary = Salary.Create(500).Value;

            Calculator calculator = new CalculatorFactory().Create(_employee);

            Salary salary = calculator.ComputeSalary(absent);

            salary.Value.Should().Be(expectedSalary);
        }
    }
}