using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrivateCloud.Practises.Validation
{
    public static class ValidationUtil
    {
        public static bool TryValidate(
            object value)
        {
            ICollection<ValidationResult> results;

            return TryValidate(
                value,
                out results);
        }

        public static bool TryValidate(
            object value,
            out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            if (value == null)
            {
                results.Add(
                    new ValidationResult(
                        $"{nameof(value)} must not be null"));

                return false;
            }

            var context = BuildContext(value);

            return Validator.TryValidateObject(
                value,
                context,
                results,
                true);
        }

        public static void Validate(
            object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var context = BuildContext(value);

            Validator.ValidateObject(
                value,
                context,
                true);
        }

        private static ValidationContext BuildContext(
            object value)
        {
            return new ValidationContext(
                value,
                serviceProvider: null,
                items: null);
        }
    }
}