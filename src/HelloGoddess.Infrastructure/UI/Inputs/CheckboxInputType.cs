using System;
using HelloGoddess.Infrastructure.Runtime.Validation;

namespace HelloGoddess.Infrastructure.UI.Inputs
{
    
    [InputType("CHECKBOX")]
    public class CheckboxInputType : InputTypeBase
    {
        public CheckboxInputType()
            : this(new BooleanValueValidator())
        {

        }

        public CheckboxInputType(IValueValidator validator)
            : base(validator)
        {
            
        }
    }
}