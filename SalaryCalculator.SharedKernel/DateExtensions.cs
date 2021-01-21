using System;

namespace SalaryCalculator.SharedKernel
{
    public static class DateExtensions
    {
        public static DateTime ToMinimumHourValue(this DateTime input)
        {
            return new DateTime(input.Year, input.Month, input.Day, 0, 0, 0);
        }
    }
}