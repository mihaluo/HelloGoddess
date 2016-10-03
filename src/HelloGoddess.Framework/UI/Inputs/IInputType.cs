using System.Collections.Generic;
using HelloGoddess.Infrastructure.Runtime.Validation;

namespace HelloGoddess.Infrastructure.UI.Inputs
{
    public interface IInputType
    {
        string Name { get; }

        object this[string key] { get; set; }

        IDictionary<string, object> Attributes { get; }

        IValueValidator Validator { get; set; }
    }
}