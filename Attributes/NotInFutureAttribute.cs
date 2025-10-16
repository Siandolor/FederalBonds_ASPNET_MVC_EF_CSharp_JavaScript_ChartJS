using System;
using System.ComponentModel.DataAnnotations;

namespace FederalBonds.Attributes
{
    // ============================================================
    // ===== Custom validation attribute to ensure that a given
    // ===== DateTime value is not set in the future.
    // ===== Commonly used for validation of purchase or birth dates.
    // ============================================================
    public class NotInFutureAttribute : ValidationAttribute
    {
        // ===== Validates that the provided value is not a future date
        public override bool IsValid(object? value)
        {
            if (value is null) return true; // ===== Null values are treated as valid (optional field)
            
            if (value is DateTime dt)
            {
                // ===== Validation passes if the date is today or earlier
                return dt.Date <= DateTime.Today;
            }

            // ===== Non-DateTime types are ignored by this validator
            return true;
        }

        // ===== Custom error message displayed when validation fails
        public override string FormatErrorMessage(string name)
        {
            return $"{name} must not be set in the future.";
        }
    }
}
