namespace SalaryCalculator.Core
{
    public class CalculatorFactory
    {
        public Calculator Create(Employee employee)
        {
            switch (employee.EmployeeType)
            {
                case EmployeeType.Regular:
                    return new RegularCalculator();

                case EmployeeType.Contractual:
                    return new ContractualCalculator();

                default:
                    return new DefaultCalculator();
            }
        }
    }
}