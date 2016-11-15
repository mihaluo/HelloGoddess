using System;
using HelloGoddess.Common.Util;
using HelloGoddess.Core.Domain.Entity;
using HelloGoddess.Core.Dto;
using HelloGoddess.Core.MongoDb;
using HelloGoddess.Core.MongoDb.Repositories;
using MongoDB.Bson;

namespace HelloGoddess.Core.Application
{
    public class BambooApplicationService : ApplicationService
    {
        IRepository<BambooDayRank> repository = new MongoDbRepositoryBase<BambooDayRank>(MongoDatabaseProvider);

        public void Add(BambooDayRankDto audienceRankDto)
        {
            var bambooDayRank = audienceRankDto.Map<BambooDayRank>();

            var insert = repository.Insert(bambooDayRank);
        }
        public void AddOrUpdate(BambooDayRankDto BambooDayRankDto)
        {

            BambooDayRank bambooDayRank = BambooDayRankDto.Map<BambooDayRank>();

            if (bambooDayRank.Id == ObjectId.Empty)
            {
                bambooDayRank.Id = ObjectId.GenerateNewId(DateTime.Now);
                var insert = repository.Insert(bambooDayRank);
                return;
            }
            repository.Update(bambooDayRank);

        }

        public BambooDayRankDto GetBambooDayRankDtoByRoomId(string fansId, string roomId, long timeStamp)
        {
            BambooDayRank bambooDayRank = repository
                                 .FirstOrDefault(rank => rank.RoomId == roomId &&
                                                rank.TimeStamp == timeStamp &&
                                                rank.FansId == fansId);

            return bambooDayRank == null ? null : bambooDayRank.Map<BambooDayRankDto>();
        }
    }
}