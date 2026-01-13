using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SchoolManagement
{
    // Helper to map ModelState validation errors to a structured JSON object
    public static class ValidationHelper
    {
        public static object FromModelState(ModelStateDictionary modelState)
        {
            var errors = modelState
                .Where(kv => kv.Value.Errors != null && kv.Value.Errors.Count > 0)
                .ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? (e.Exception?.Message ?? "Invalid value") : e.ErrorMessage).ToArray()
                );

            return new { message = "Validation failed", errors };
        }
    }
}
