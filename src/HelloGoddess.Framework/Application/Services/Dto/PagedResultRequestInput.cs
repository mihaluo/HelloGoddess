using System;
using System.ComponentModel.DataAnnotations;

namespace HelloGoddess.Infrastructure.Application.Services.Dto
{
    /// <summary>
    /// Simply implements <see cref="IPagedResultRequest"/>.
    /// </summary>
    
    public class PagedResultRequestInput : LimitedResultRequestInput, IPagedResultRequest
    {
        [Range(0, int.MaxValue)]
        public virtual int SkipCount { get; set; }
    }
}