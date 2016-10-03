using System;

namespace HelloGoddess.Infrastructure.Application.Services.Dto
{
    /// <summary>
    /// Simply implements <see cref="IPagedAndSortedResultRequest"/>.
    /// </summary>
    
    public class PagedAndSortedResultRequestInput : PagedResultRequestInput, IPagedAndSortedResultRequest
    {
        public virtual string Sorting { get; set; }
    }
}