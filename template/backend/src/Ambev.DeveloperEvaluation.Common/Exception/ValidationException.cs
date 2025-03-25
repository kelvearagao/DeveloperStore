using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Common.Exceptions;

/// <summary>
/// Exception thrown when validation errors occur.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// A collection of validation errors.
    /// </summary>
    public IEnumerable<string> Errors { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a collection of errors.
    /// </summary>
    /// <param name="errors">The validation errors.</param>
    public ValidationException(IEnumerable<string> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a single error message.
    /// </summary>
    /// <param name="error">The validation error.</param>
    public ValidationException(string error)
        : this(new List<string> { error })
    {
    }

    /// <summary>
    /// Returns a string representation of the validation errors.
    /// </summary>
    /// <returns>A string containing all validation errors.</returns>
    public override string ToString()
    {
        return $"{Message}{Environment.NewLine}{string.Join(Environment.NewLine, Errors)}";
    }
}