using FluentAssertions;
using Moq;
using SalaryCalculator.SharedKernel;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SalaryCalculator.Core.UnitTests
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly EmployeeService _employeeService;
        private Employee _employee;

        public EmployeeServiceTest()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_mockRepository.Object);

            _employee = new Employee()
            {
                Name = Name.Create("Joshua Santos").Value,
                BirthDate = BirthDate.Create(new DateTime(1994, 2, 8)).Value,
                TIN = TIN.Create("123456789").Value
            };
        }

        [Fact]
        public async Task WhenFind_ThenCallRepositoryFind()
        {
            Guid randomId = Guid.NewGuid();
            await _employeeService.Find(randomId);

            _mockRepository.Verify(mock => mock.Find(randomId), Times.Once);
        }

        [Fact]
        public async Task WhenFind_ThenCallRepositoryGetAll()
        {
            await _employeeService.GetAll();

            _mockRepository.Verify(mock => mock.GetAll());
        }

        [Fact]
        public async Task WhenCreate_ThenCallRepositoryCreate()
        {
            Employee employee = new Employee();
            await _employeeService.Create(employee);

            _mockRepository.Verify(mock => mock.Create(employee), Times.Once);
        }

        [Fact]
        public async Task WhenUpdate_ThenCallRepositoryUpdate()
        {
            Employee employee = new Employee();
            await _employeeService.Update(employee);

            _mockRepository.Verify(mock => mock.Update(employee), Times.Once);
        }

        [Fact]
        public async Task WhenDelete_ThenCallRepositoryDelete()
        {
            Guid randomId = Guid.NewGuid();
            await _employeeService.Delete(randomId);

            _mockRepository.Verify(mock => mock.Delete(randomId), Times.Once);
        }

        [Theory]
        [InlineData(1, 16_690.91)]
        [InlineData(0, 17_600)]
        [InlineData(24, -4_218.18)]
        public void WhenCalculateSalaryOfRegularEmployee_CalculateCorrectly(
            decimal absent,
            decimal expectedSalary)
        {
            _employee.EmployeeType = EmployeeType.Regular;
            _employee.Salary = 20_000m;

            Calculator calculator = new CalculatorFactory().Create(_employee);

            decimal salary = calculator.ComputeSalary(absent);

            salary.Should().Be(expectedSalary);
        }

        [Theory]
        [InlineData(15.5, 500, 7_750)]
        [InlineData(22, 500, 11_000)]
        [InlineData(15.75, 537.38, 8_463.74)]
        public void WhenCalculateSalaryOfContractualEmployee_CalculateCorrectly(
            decimal absent,
            decimal dailySalary,
            decimal expectedSalary)
        {
            _employee.EmployeeType = EmployeeType.Contractual;
            _employee.Salary = dailySalary;

            Calculator calculator = new CalculatorFactory().Create(_employee);

            decimal salary = calculator.ComputeSalary(absent);

            salary.Should().Be(expectedSalary);
        }
    }
}