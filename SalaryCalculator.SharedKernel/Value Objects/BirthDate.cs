using CSharpFunctionalExtensions;
using System;

namespace SalaryCalculator.SharedKernel
{
    public class BirthDate : ValueObject<BirthDate>
    {
        private const int MinimumYear = 1900;

        public DateTime Value { get; }

        private BirthDate(DateTime value)
        {
            Value = value;
        }

        public static Result<BirthDate> Create(DateTime birthDate)
        {
            if (birthDate.Year < MinimumYear)
                return Result.Failure<BirthDate>($"Birth date cannot be earlier than year {MinimumYear}");

            if (birthDate.ToMinimumHourValue() >= DateTime.Now.ToMinimumHourValue())
                return Result.Failure<BirthDate>("Birth date cannot be greater than or equal today");

            return Result.Success(new BirthDate(birthDate));
        }

        protected override bool EqualsCore(BirthDate other)
        {
            return Value.ToMinimumHourValue() == other.Value.ToMinimumHourValue();
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}