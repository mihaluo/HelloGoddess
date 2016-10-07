using System;
using HelloGoddess.Core.Application;
using HelloGoddess.Core.Dto;
using HelloGoddess.Crawlar.Model.Gift;
using HelloGoddess.Common.Util;
using HelloGoddess.Common.Redis;

namespace HelloGoddess.Crawlar.Core.GiftProcesser
{
    //[Disable]
    public class DayRankProcesser : IGiftProcesser
    {
        private const string LockPrex = "DayRankLock";

        public void Process(Gift gift)
        {

            DayRankApplicationService dayRankApplicationService = new DayRankApplicationService();

            long timeStamp = TimeStampHelper.GetTodayTimeStampSeconds();
            string roomId = Dict.GoddessNameDict.ContainsKey(gift.to.toroom) ? gift.to.toroom : Dict.MainRoomGoddessRoomIdMap[gift.content.name];
            string goddessName = Dict.GoddessNameDict[roomId];
            RedisLock.SingleLock(LockPrex + roomId + timeStamp,
             () =>
             {
                 var dayRankDto = dayRankApplicationService.GetDayRankDtoByRoomIdAndDate(roomId, timeStamp) ??
                                new DayRankDto
                                {
                                      TimeStamp = timeStamp,
                                      GoddessName = goddessName,
                                      RoomId = roomId
                                };

                 dayRankDto.GoddessValue += (long)(gift.content.price * gift.content.count);
                 dayRankApplicationService.AddOrUpdate(dayRankDto);
             }, null);
        }
    }
}