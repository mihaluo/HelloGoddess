using System.Collections.Generic;

namespace HelloGoddess.Infrastructure.Application.Services
{
    public interface IAvoidDuplicateCrossCuttingConcerns
    {
        List<string> AppliedCrossCuttingConcerns { get; }
    }
}