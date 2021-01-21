using CSharpFunctionalExtensions;
using System;

namespace SalaryCalculator.SharedKernel
{
    public class Salary : ValueObject<Salary>
    {
        public const int MaximumAmount = 10_000_000;

        public decimal Value { get; }

        private Salary(decimal value)
        {
            Value = value;
        }

        public static Result<Salary> Create(decimal value)
        {
            value = Math.Round(value, 2, MidpointRounding.AwayFromZero);

            if (value <= 0)
                return Result.Failure<Salary>("Salary should not be less than or equal to zero");

            if (value > MaximumAmount)
                return Result.Failure<Salary>($"Salary should not be greater than maximum amount [{MaximumAmount}]");

            return Result.Success(new Salary(value));
        }

        protected override bool EqualsCore(Salary other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}