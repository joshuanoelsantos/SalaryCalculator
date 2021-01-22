using SalaryCalculator.SharedKernel;

namespace SalaryCalculator.Core
{
    public abstract class Calculator
    {
        protected readonly Employee _employee;

        public Calculator(Employee employee)
        {
            _employee = employee;
        }

        public abstract decimal ComputeSalary(decimal input);
    }
}