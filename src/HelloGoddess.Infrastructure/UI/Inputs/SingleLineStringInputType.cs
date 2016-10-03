using System;
using HelloGoddess.Infrastructure.Runtime.Validation;

namespace HelloGoddess.Infrastructure.UI.Inputs
{
    
    [InputType("SINGLE_LINE_STRING")]
    public class SingleLineStringInputType : InputTypeBase
    {
        public SingleLineStringInputType()
        {

        }

        public SingleLineStringInputType(IValueValidator validator)
            : base(validator)
        {
        }
    }
}