using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalaryCalculator.Core
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Employee> Find(Guid employeeId)
        {
            return await _repository.Find(employeeId);
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Employee> Create(Employee employee)
        {
            return await _repository.Create(employee);
        }

        public async Task<Employee> Edit(Employee employee)
        {
            return await _repository.Update(employee);
        }

        public async Task Delete(Guid employeeId)
        {
            await _repository.Delete(employeeId);
        }

        public Task<decimal> CalculateSalary(Employee employee, decimal input)
        {
            Calculator calculator = new CalculatorFactory().Create(employee);

            decimal salary = calculator.ComputeSalary(input);
            return Task.FromResult(salary);
        }
    }
}