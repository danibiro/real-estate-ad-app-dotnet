// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;

namespace bdim1996_dotnet.Models.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class PresentOrFutureAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Date must be present or future");
        }
        DateOnly? date = (DateOnly)value;
        if (date < DateOnly.FromDateTime(DateTime.Now))
        {
            return new ValidationResult("Date must be present or future");
        }
        else
        {
            return ValidationResult.Success!;
        }
    }
}
