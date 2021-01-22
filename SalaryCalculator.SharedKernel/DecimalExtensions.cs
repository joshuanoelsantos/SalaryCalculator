using System;

namespace SalaryCalculator.SharedKernel
{
    public static class DecimalExtensions
    {
        public static decimal Round(this decimal value, int decimalPlaces = 2)
        {
            return Math.Round(value, decimalPlaces, MidpointRounding.AwayFromZero);
        }
    }
}