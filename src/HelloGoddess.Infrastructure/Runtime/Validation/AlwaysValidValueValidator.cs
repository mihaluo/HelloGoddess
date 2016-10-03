using System;

namespace HelloGoddess.Infrastructure.Runtime.Validation
{
    [Validator("NULL")]
    
    public class AlwaysValidValueValidator : ValueValidatorBase
    {
        public override bool IsValid(object value)
        {
            return true;
        }
    }
}