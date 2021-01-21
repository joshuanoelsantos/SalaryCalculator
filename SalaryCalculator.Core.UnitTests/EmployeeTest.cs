using CSharpFunctionalExtensions;
using FluentAssertions;
using SalaryCalculator.SharedKernel;
using System;
using Xunit;

namespace SalaryCalculator.Core.UnitTests
{
    public class EmployeeTest
    {
        private const string _500Characters =
            "12345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890" +
            "12345678901234567890123456789012345678901234567890";

        [Fact]
        public void WhenBirthDateIsValid_ThenReturnResultTrue()
        {
            DateTime birthDate = new DateTime(1994, 2, 8);

            Result<BirthDate> birthDateOrError = BirthDate.Create(birthDate);

            AssertTrueValueObject(birthDateOrError);
            birthDateOrError.Value.Value.Should().Be(birthDate);
        }

        [Fact]
        public void WhenBirthDateIsEarlierThanYear1900_ThenReturnResultFailure()
        {
            DateTime birthDate = new DateTime(1899, 12, 31);

            Result<BirthDate> birthDateOrError = BirthDate.Create(birthDate);

            string errorMessage = "Birth date cannot be earlier than year 1900";
            AssertFalseValueObject(birthDateOrError, errorMessage);
        }

        [Fact]
        public void WhenBirthDateIsLaterThanToday_ThenReturnResultFailure()
        {
            DateTime birthDate = DateTime.Now;

            Result<BirthDate> birthDateOrError = BirthDate.Create(birthDate);

            string errorMessage = "Birth date cannot be greater than or equal today";
            AssertFalseValueObject(birthDateOrError, errorMessage);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("Joshua Santos")]
        [InlineData(_500Characters)]
        public void WhenNameIsValid_ThenReturnResultTrue(string name)
        {
            Result<Name> nameOrError = Name.Create(name);

            AssertTrueValueObject(nameOrError);
            nameOrError.Value.Value.Should().Be(name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void WhenNameIsNullOrEmpty_ThenReturnResultFailure(string name)
        {
            Result<Name> nameOrError = Name.Create(name);

            string errorMessage = "Name should not be empty";
            AssertFalseValueObject(nameOrError, errorMessage);
        }

        [Fact]
        public void WhenNameIsMoreThan500Characters_ThenReturnResultFailure()
        {
            string name = _500Characters + "1";

            Result<Name> nameOrError = Name.Create(name);

            string errorMessage = "Name should not be more than 500 characters";
            AssertFalseValueObject(nameOrError, errorMessage);
        }

        [Theory]
        [InlineData("123456789")]
        [InlineData("123-456-789")]
        [InlineData("12345678900000")]
        [InlineData("123-456-789-00000")]
        public void WhenTINIsValid_ThenReturnResultTrue(string tin)
        {
            Result<TIN> tinOrError = TIN.Create(tin);

            AssertTrueValueObject(tinOrError);
            tinOrError.Value.Value.Should().Be(tin);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void WhenTINIsNullOrEmpty_ThenReturnResultFailure(string tin)
        {
            Result<TIN> tinOrError = TIN.Create(tin);

            string errorMessage = "TIN should not be empty";
            AssertFalseValueObject(tinOrError, errorMessage);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        [InlineData("12345678")]
        public void WhenTINIsLessThan9Characters_ThenReturnResultFailure(string tin)
        {
            Result<TIN> tinOrError = TIN.Create(tin);

            string errorMessage = "TIN should not be less than 9 characters";
            AssertFalseValueObject(tinOrError, errorMessage);
        }

        [Fact]
        public void WhenTINIsMoreThan17Characters_ThenReturnResultFailure()
        {
            string tin = "123456789012345678";

            Result<TIN> tinOrError = TIN.Create(tin);

            string errorMessage = "TIN should not be more than 17 characters";
            AssertFalseValueObject(tinOrError, errorMessage);
        }

        #region Private Methods

        private static void AssertTrueValueObject<T>(Result<T> createResult)
        {
            createResult.IsFailure.Should().BeFalse();
            Assert.Throws<ResultSuccessException>(() => createResult.Error);

            Result result = Result.Combine(createResult);

            result.IsFailure.Should().BeFalse();
            Assert.Throws<ResultSuccessException>(() => createResult.Error);
        }

        private static void AssertFalseValueObject<T>(Result<T> createResult, string errorMessage)
        {
            createResult.IsFailure.Should().BeTrue();
            createResult.Error.Should().Be(errorMessage);

            Result result = Result.Combine(createResult);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(errorMessage);
        }

        #endregion Private Methods
    }
}