using CSharpFunctionalExtensions;
using System;

namespace SalaryCalculator.SharedKernel
{
    public class TIN : ValueObject<TIN>

    {
        private const int MinimumCharacters = 9;
        private const int MaximumCharacters = 17;

        public string Value { get; }

        private TIN(string value)
        {
            Value = value;
        }

        public static Result<TIN> Create(string tin)
        {
            tin = (tin ?? string.Empty).Trim();

            if (tin.Length == 0)
                return Result.Failure<TIN>("TIN should not be empty");

            if (tin.Length < MinimumCharacters)
                return Result.Failure<TIN>($"TIN should not be less than {MinimumCharacters} characters");

            if (tin.Length > MaximumCharacters)
                return Result.Failure<TIN>($"TIN should not be more than {MaximumCharacters} characters");

            return Result.Success(new TIN(tin));
        }

        protected override bool EqualsCore(TIN other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}