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

        public static Salary operator +(Salary salary1, Salary salary2)
        {
            if (salary1 == null)
                return null;

            if (salary2 == null)
                return null;

            return Create(salary1.Value + salary2.Value).Value;
        }

        public static Salary operator -(Salary salary1, Salary salary2)
        {
            if (salary1 == null)
                return null;

            if (salary2 == null)
                return null;

            return Create(salary1.Value - salary2.Value).Value;
        }

        public static bool operator >(Salary salary1, Salary salary2)
        {
            return salary1?.Value > salary2?.Value;
        }

        public static bool operator <(Salary salary1, Salary salary2)
        {
            return salary1?.Value < salary2?.Value;
        }

        public static bool operator >=(Salary salary1, Salary salary2)
        {
            return salary1?.Value >= salary2?.Value;
        }

        public static bool operator <=(Salary salary1, Salary salary2)
        {
            return salary1?.Value <= salary2?.Value;
        }

        public static bool operator ==(Salary salary1, Salary salary2)
        {
            return salary1?.Value == salary2?.Value;
        }

        public static bool operator !=(Salary salary1, Salary salary2)
        {
            return salary1?.Value != salary2?.Value;
        }

        public static implicit operator decimal(Salary m) => m.Value;

        public static implicit operator Salary(decimal d) => Create(d).Value;

        public override string ToString()
        {
            return $"value: {Value}";
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