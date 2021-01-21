using CSharpFunctionalExtensions;
using System;

namespace SalaryCalculator.SharedKernel
{
    public class Name : ValueObject<Name>
    {
        private const int MaximumCharacters = 500;

        public string Value { get; }

        private Name(string value)
        {
            Value = value;
        }

        public static Result<Name> Create(string name)
        {
            name = (name ?? string.Empty).Trim();

            if (name.Length == 0)
                return Result.Failure<Name>("Name should not be empty");

            if (name.Length > MaximumCharacters)
                return Result.Failure<Name>($"Name should not be more than {MaximumCharacters} characters");

            return Result.Success(new Name(name));
        }

        protected override bool EqualsCore(Name other)
        {
            return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}