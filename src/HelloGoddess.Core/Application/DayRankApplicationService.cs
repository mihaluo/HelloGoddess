using HelloGoddess.Common.Util;
using HelloGoddess.Core.Domain.Entity;
using HelloGoddess.Core.Dto;
using HelloGoddess.Core.MongoDb.Repositories;
using HelloGoddess.Infrastructure.Domain.Repositories;

namespace HelloGoddess.Core.Application
{
    public class DayRankApplicationService : ApplicationService
    {

        public bool AddOrUpdate(DayRankDto dayRankDto)
        {
            IRepository<DayRank> repository = new MongoDbRepositoryBase<DayRank>(base.MongoDatabaseProvider);
            DayRank dayRank = dayRankDto.Map<DayRank>();
            DayRank insert = repository.Insert(dayRank);
            //repository.Insert()
            return true;
        }
    }
}