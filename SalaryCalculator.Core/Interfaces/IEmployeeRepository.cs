using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalaryCalculator.Core
{
    public interface IEmployeeRepository
    {
        Task<Employee> Find(Guid employeeId);

        Task<List<Employee>> GetAll();

        Task<List<Employee>> Search(string searchedName);

        Task<Employee> Create(Employee employee);

        Task<Employee> Update(Employee employee);

        Task Delete(Guid employeeId);
    }
}