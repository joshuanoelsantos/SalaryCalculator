using SalaryCalculator.Core;
using SalaryCalculator.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryCalculator.Infrastructure
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees;

        public EmployeeRepository()
        {
            _employees = new List<Employee>()
            {
                new Employee()
                {
                    Name = Name.Create("Joshua Santos").Value,
                    BirthDate = BirthDate.Create(new DateTime(1994, 2, 8)).Value,
                    TIN = TIN.Create("123456789").Value,
                    EmployeeType = EmployeeType.Regular,
                    Salary = 20_000m
                }
            };
        }

        public Task<Employee> Find(Guid employeeId)
        {
            var employee = _employees
                .Where(x => x.Id == employeeId)
                .FirstOrDefault();

            return Task.FromResult(employee);
        }

        public Task<List<Employee>> GetAll()
        {
            return Task.FromResult(_employees.ToList());
        }

        public Task<Employee> Create(Employee employee)
        {
            _employees.Add(employee);

            return Task.FromResult(employee);
        }

        public async Task<Employee> Update(Employee updatedEmployee)
        {
            var employee = await Find(updatedEmployee.Id);

            employee.Name = updatedEmployee.Name;
            employee.BirthDate = updatedEmployee.BirthDate;
            employee.TIN = updatedEmployee.TIN;
            employee.EmployeeType = updatedEmployee.EmployeeType;
            employee.Salary = updatedEmployee.Salary;

            return employee;
        }

        public async Task Delete(Guid employeeId)
        {
            var employee = await Find(employeeId);

            _employees.Remove(employee);
        }
    }
}