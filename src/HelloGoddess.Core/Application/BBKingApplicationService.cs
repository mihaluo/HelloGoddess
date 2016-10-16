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
    public class BBKingApplicationService : ApplicationService
    {

        IRepository<BBKingDayRank> repository = new MongoDbRepositoryBase<BBKingDayRank>(MongoDatabaseProvider);

        public void AddOrUpdate(BBKingDayRankDto bbKingDayRankDto)
        {

            BBKingDayRank bbKingDayRank = bbKingDayRankDto.Map<BBKingDayRank>();
            bbKingDayRank.LastTimeStamp = TimeStampHelper.GetCurrentDateTimeStampSeconds();

            if (bbKingDayRank.Id == ObjectId.Empty)
            {
                bbKingDayRank.Id = ObjectId.GenerateNewId(DateTime.Now);
                BBKingDayRank insert = repository.Insert(bbKingDayRank);
                return;
            }
            repository.Update(bbKingDayRank);

        }

        public BBKingDayRankDto GetBBKingDayRankDtoByRoomId(string fansId, string roomId, long timeStamp)
        {
            BBKingDayRank bbkingDayRank = repository
                                 .FirstOrDefault(rank => rank.RoomId == roomId &&
                                                rank.TimeStamp == timeStamp &&
                                                rank.FansId == fansId);

            return bbkingDayRank == null ? null : bbkingDayRank.Map<BBKingDayRankDto>();
        }

    }
}