using System;
using HelloGoddess.Common.Util;
using HelloGoddess.Core.Domain.Entity;
using HelloGoddess.Core.Dto;
using HelloGoddess.Core.MongoDb;
using HelloGoddess.Core.MongoDb.Repositories;
using MongoDB.Bson;
using HelloGoddess.Common.Redis;

namespace HelloGoddess.Core.Application
{
    public class AudienceRankApplicationService : ApplicationService
    {

        IRepository<AudienceRank> repository = new MongoDbRepositoryBase<AudienceRank>(MongoDatabaseProvider);

        public void Add(AudienceRankDto audienceRankDto)
        {
            AudienceRank audienceRank = audienceRankDto.Map<AudienceRank>();

            AudienceRank insert = repository.Insert(audienceRank);
        }



    }
}