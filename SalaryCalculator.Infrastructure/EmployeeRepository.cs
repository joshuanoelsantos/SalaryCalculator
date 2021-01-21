﻿using SalaryCalculator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryCalculator.Infrastructure
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public List<Employee> employees;

        public EmployeeRepository()
        {
            employees = new List<Employee>();
        }

        public Task<Employee> Find(Guid employeeId)
        {
            var employee = employees
                .Where(x => x.Id == employeeId)
                .FirstOrDefault();

            return Task.FromResult(employee);
        }

        public Task<List<Employee>> GetAll()
        {
            return Task.FromResult(employees.ToList());
        }

        public Task<Employee> Create(Employee employee)
        {
            employees.Add(employee);

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

            employees.Remove(employee);
        }
    }
}