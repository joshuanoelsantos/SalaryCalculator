using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalaryCalculator.Core
{
    public interface IEmployeeService
    {
        Task<decimal> CalculateSalary(Employee employee, decimal input);

        Task<Employee> Create(Employee employee);

        Task Delete(Guid employeeId);

        Task<Employee> Update(Employee employee);

        Task<Employee> Find(Guid employeeId);

        Task<List<Employee>> GetAll();
    }
}