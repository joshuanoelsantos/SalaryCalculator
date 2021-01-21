namespace SalaryCalculator.Core
{
    public class CalculatorFactory
    {
        public Calculator Create(Employee employee)
        {
            switch (employee.EmployeeType)
            {
                case EmployeeType.Regular:
                    return new RegularCalculator(employee);

                case EmployeeType.Contractual:
                    return new ContractualCalculator(employee);

                default:
                    return new DefaultCalculator(employee);
            }
        }
    }
}