using System.ComponentModel.DataAnnotations;

namespace HelloGoddess.Infrastructure.Application.Services.Dto
{
    /// <summary>
    /// Simply implements <see cref="ILimitedResultRequest"/>.
    /// </summary>
    public class LimitedResultRequestInput : ILimitedResultRequest
    {
        [Range(1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = 10;
    }
}