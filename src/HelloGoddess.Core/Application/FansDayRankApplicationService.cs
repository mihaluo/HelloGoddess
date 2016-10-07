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
    public class FansDayRankApplicationService : ApplicationService
    {

        IRepository<FansDayRank> repository = new MongoDbRepositoryBase<FansDayRank>(MongoDatabaseProvider);

        public void AddOrUpdate(FansDayRankDto fansDayRankDto)
        {

            FansDayRank fansDayRank = fansDayRankDto.Map<FansDayRank>();
            fansDayRank.LastTimeStamp = TimeStampHelper.GetCurrentDateTimeStampSeconds();

            if (fansDayRank.Id == ObjectId.Empty)
            {
                fansDayRank.Id = ObjectId.GenerateNewId(DateTime.Now);
                FansDayRank insert = repository.Insert(fansDayRank);
                return;
            }
            repository.Update(fansDayRank);

        }

        public FansDayRankDto GetFansDayRankDtoByRoomId(string fansId, string roomId, long timeStamp)
        {
            FansDayRank fansDayRank = repository
                                 .FirstOrDefault(rank => rank.GoddessRoomId == roomId &&
                                                rank.TimeStamp == timeStamp &&
                                                rank.FansId == fansId);

            return fansDayRank == null ? null : fansDayRank.Map<FansDayRankDto>();
        }

    }
}