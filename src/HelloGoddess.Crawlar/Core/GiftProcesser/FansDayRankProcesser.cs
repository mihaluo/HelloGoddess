using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloGoddess.Crawlar.Model.Gift;
using HelloGoddess.Core.Application;
using HelloGoddess.Core.Dto;

namespace HelloGoddess.Crawlar.Core.GiftProcesser
{
    public class FansDayRankProcesser : IGiftProcesser
    {
        public void Process(Gift gift)
        {
            FansDayRankApplicationService fansDayRankApplicationService = new FansDayRankApplicationService();

            long timeStamp = DateTimeOffset.Parse(DateTime.Today.ToString()).ToUnixTimeSeconds();
            string roomId = Dict.GoddessNameDict.ContainsKey(gift.to.toroom) ? gift.to.toroom : Dict.MainRoomGoddessRoomIdMap[gift.content.name];
            string goddessName = Dict.GoddessNameDict[roomId];

            var fansDayRankDto = fansDayRankApplicationService.GetFansDayRankDtoByRoomId(gift.from.rid, gift.to.toroom, timeStamp) ??
             new FansDayRankDto
             {
                 TimeStamp = timeStamp,
                 GoddessRoomId = roomId,
                 GoddessName = goddessName,
                 FansId = gift.from.rid,
                 RansName = gift.from.nickName
             };

            fansDayRankDto.ContributionGoddessValue += (long)(gift.content.price * gift.content.count);
            fansDayRankApplicationService.AddOrUpdate(fansDayRankDto);
        }
    }
}
