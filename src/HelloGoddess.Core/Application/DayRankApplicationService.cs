using System;
using HelloGoddess.Common.Util;
using HelloGoddess.Core.Domain.Entity;
using HelloGoddess.Core.Dto;
using HelloGoddess.Core.MongoDb;
using HelloGoddess.Core.MongoDb.Repositories;
using MongoDB.Bson;

namespace HelloGoddess.Core.Application
{
    public class DayRankApplicationService : ApplicationService
    {

        IRepository<DayRank> repository = new MongoDbRepositoryBase<DayRank>(MongoDatabaseProvider);

        public void AddOrUpdate(DayRankDto dayRankDto)
        {
            DayRank dayRank = dayRankDto.Map<DayRank>();
            if (dayRank.Id == ObjectId.Empty)
            {
                dayRank.Id = ObjectId.GenerateNewId(DateTime.Now);
                DayRank insert = repository.Insert(dayRank);
                return;
            }
            repository.Update(dayRank);
        }

        public DayRankDto GetDayRankDtoByRoomIdAndDate(string roomId, long timeStamp)
        {
            DayRank dayRank = repository.FirstOrDefault(rank => rank.RoomId == roomId && rank.TimeStamp == timeStamp);
            return dayRank == null ? null : dayRank.Map<DayRankDto>();
        }

    }
}