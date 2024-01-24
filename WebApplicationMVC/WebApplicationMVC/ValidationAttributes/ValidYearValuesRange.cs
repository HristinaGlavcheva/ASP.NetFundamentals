using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.ValidationAttributes
{
    public class ValidDateValuesRange : ValidationAttribute
    {
        private readonly int minYear;

        public ValidDateValuesRange(int minYear)
        {
            this.ErrorMessage = $"Date should be between year {minYear} and current date - {DateTime.UtcNow}";
            this.MinYear = minYear;
        }

        public int MinYear { get; set; }

        public override bool IsValid(object value)
        {
            if (value is int intValue) // <=> checks if value is int, and if it is enters the "if codeblock" and also writes the value in the intValue variable
            {
                if (intValue <= DateTime.UtcNow.Year) 
                {
                    if(intValue >= MinYear)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
