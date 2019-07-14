using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace SimpleVidly.Models
{
    public class Min18YearsIfSubscribed : ValidationAttribute
    {
        //this gives you access to other properties of your model
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //gives access to containing class
            //it's an object, so need to case it
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birth Date is required.");
            }

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;
            if (customer.IsSubscribedToNewsletter == true)
            {
                return (age >= 18) ? ValidationResult.Success 
                    : new ValidationResult("Customer should be at least 18 years old to be subscribed to the newsletter");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
        //he put success first and added if they haven't selected something yet, also make it success so they don't have too many error messages at once.  It won't submit because membership type is required.Once they select a time and its an invalid combo, then the error message will show up.
    }
}