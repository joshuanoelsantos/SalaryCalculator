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

        public static Result<BirthDate> Create(DateTime value)
        {
            if (value.Year < MinimumYear)
                return Result.Failure<BirthDate>($"Birth date should not be earlier than year {MinimumYear}");

            if (value.ToMinimumHourValue() >= DateTime.Now.ToMinimumHourValue())
                return Result.Failure<BirthDate>("Birth date should not be greater than or equal today");

            return Result.Success(new BirthDate(value));
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