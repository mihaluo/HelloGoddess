using System;
using HelloGoddess.Core.Application;
using HelloGoddess.Core.Dto;
using HelloGoddess.Crawlar.Model.Gift;
using HelloGoddess.Common.Util;

namespace HelloGoddess.Crawlar.Core.GiftProcesser
{
    //[Disable]
    public class DayRankProcesser : IGiftProcesser
    {
        public void Process(Gift gift)
        {

            DayRankApplicationService dayRankApplicationService = new DayRankApplicationService();

            long timeStamp = DateTimeOffset.Parse(DateTime.Today.ToString()).ToUnixTimeSeconds();
            string roomId = Dict.GoddessNameDict.ContainsKey(gift.to.toroom) ? gift.to.toroom : Dict.MainRoomGoddessRoomIdMap[gift.content.name];
            string goddessName = Dict.GoddessNameDict[roomId];

            var dayRankDto = dayRankApplicationService.GetDayRankDtoByRoomIdAndDate(roomId, timeStamp) ??
             new DayRankDto
             {
                 TimeStamp = timeStamp,
                 GoddessName = goddessName,
                 RoomId = roomId
             };

            dayRankDto.GoddessValue += (long)(gift.content.price * gift.content.count);
            dayRankApplicationService.AddOrUpdate(dayRankDto);

        }
    }
}