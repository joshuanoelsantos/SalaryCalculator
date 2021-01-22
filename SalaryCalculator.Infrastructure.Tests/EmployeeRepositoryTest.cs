using FluentAssertions;
using SalaryCalculator.Core;
using SalaryCalculator.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SalaryCalculator.Infrastructure.Tests
{
    public class EmployeeRepositoryTest
    {
        [Fact]
        public async Task EmployeeRepository_CRUD_Test()
        {
            EmployeeRepository repository = new EmployeeRepository();

            // GetAll
            List<Employee> employees = await repository.GetAll();
            employees.Count.Should().Be(1);

            // Create
            var name = Name.Create("Joshua Santos").Value;
            var birthDate = BirthDate.Create(new DateTime(1994, 2, 8)).Value;
            var tin = TIN.Create("123456789").Value;
            var salary = Salary.Create(70_000m).Value;
            var createdEmployee = await repository.Create(
                new Employee()
                {
                    Name = name,
                    BirthDate = birthDate,
                    TIN = tin,
                    Salary = salary,
                });

            employees = await repository.GetAll();
            employees.Count.Should().Be(2);

            createdEmployee.Name.Should().Be(name);
            createdEmployee.BirthDate.Should().Be(birthDate);
            createdEmployee.TIN.Should().Be(tin);
            createdEmployee.Salary.Should().Be(salary);

            // Find
            var fetchedEmployee = await repository.Find(createdEmployee.Id);
            fetchedEmployee.Should().NotBeNull();

            fetchedEmployee.Name.Should().Be(name);
            fetchedEmployee.BirthDate.Should().Be(birthDate);
            fetchedEmployee.TIN.Should().Be(tin);
            fetchedEmployee.Salary.Should().Be(salary);

            // Update
            var name2 = Name.Create("Joshua Noel Santos").Value;
            var birthDate2 = BirthDate.Create(new DateTime(1996, 2, 8)).Value;
            var tin2 = TIN.Create("123-456-789").Value;
            var salary2 = Salary.Create(75_000m).Value;

            fetchedEmployee.Name = name2;
            fetchedEmployee.BirthDate = birthDate2;
            fetchedEmployee.TIN = tin2;
            fetchedEmployee.Salary = salary2;
            var updatedEmployee = await repository.Update(fetchedEmployee);

            employees = await repository.GetAll();
            employees.Count.Should().Be(2);

            updatedEmployee.Name.Should().Be(name2);
            updatedEmployee.BirthDate.Should().Be(birthDate2);
            updatedEmployee.TIN.Should().Be(tin2);
            updatedEmployee.Salary.Should().Be(salary2);

            // Delete
            await repository.Delete(updatedEmployee.Id);

            employees = await repository.GetAll();
            employees.Count.Should().Be(1);

            fetchedEmployee = await repository.Find(updatedEmployee.Id);
            fetchedEmployee.Should().BeNull();
        }
    }
}