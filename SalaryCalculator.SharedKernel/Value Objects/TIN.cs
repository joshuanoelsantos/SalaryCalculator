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

        public static Result<TIN> Create(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.Length == 0)
                return Result.Failure<TIN>("TIN should not be empty");

            if (value.Length < MinimumCharacters)
                return Result.Failure<TIN>($"TIN should not be less than {MinimumCharacters} characters");

            if (value.Length > MaximumCharacters)
                return Result.Failure<TIN>($"TIN should not be more than {MaximumCharacters} characters");

            return Result.Success(new TIN(value));
        }

        public override string ToString()
        {
            return $"value: {Value}";
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