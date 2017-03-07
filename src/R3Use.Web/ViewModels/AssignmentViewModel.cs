using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace R3Use.Web.ViewModels
{
    public class AssignmentViewModel : ErrorCodeViewModel
    {

        [Required]
        public int Id { get; set; }

        [MyRequired(ErrorCode = 1001)]
        public string Name { get; set; }

    }

    public class ErrorCodeViewModel
    {
        public ErrorCodeViewModel()
        {
            ErrorCodes = new Dictionary<string, int>();
        }
        public IDictionary<string, int> ErrorCodes { get; set; }
    }



    public class MyRequired : RequiredAttribute, IErrorCode
    {
        public int ErrorCode { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var result = base.IsValid(value, validationContext);

            if ((result != null && result.MemberNames.Any()) && validationContext.ObjectInstance is ErrorCodeViewModel )
            {
                ((ErrorCodeViewModel)validationContext.ObjectInstance).ErrorCodes.Add(result.MemberNames.First(), ErrorCode);
            }

            return result;
        }
    }

    public interface IErrorCode 
    {
        int ErrorCode { get; set; }
    }

    public class MyValidationResult : ValidationResult
    {
        public MyValidationResult(ValidationResult validationResult) : base(validationResult)
        {
        }

        public MyValidationResult(string errorMessage) : base(errorMessage)
        {
        }

        public MyValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
        {
        }

        public int ErrorCode { get; set; }
    }
}