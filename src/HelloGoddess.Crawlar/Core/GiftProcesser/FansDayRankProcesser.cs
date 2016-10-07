using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloGoddess.Crawlar.Model.Gift;
using HelloGoddess.Core.Application;
using HelloGoddess.Core.Dto;
using HelloGoddess.Common.Util;
using HelloGoddess.Common.Redis;

namespace HelloGoddess.Crawlar.Core.GiftProcesser
{
    //[Disable]
    public class FansDayRankProcesser : IGiftProcesser
    {
        private const string LockPrex = "FansDayLock";

        public void Process(Gift gift)
        {
            FansDayRankApplicationService fansDayRankApplicationService = new FansDayRankApplicationService();

            long timeStamp = TimeStampHelper.GetTodayTimeStampSeconds();
            string roomId = Dict.GoddessNameDict.ContainsKey(gift.to.toroom) ? gift.to.toroom : Dict.MainRoomGoddessRoomIdMap[gift.content.name];
            string goddessName = Dict.GoddessNameDict[roomId];

            RedisLock.SingleLock(LockPrex + gift.from.rid + roomId + timeStamp,
               () =>
               {                   
                   var fansDayRankDto = fansDayRankApplicationService.GetFansDayRankDtoByRoomId(gift.from.rid, roomId, timeStamp) ??
                        new FansDayRankDto
                        {
                            TimeStamp = timeStamp,
                            GoddessRoomId = roomId,
                            GoddessName = goddessName,
                            FansId = gift.from.rid,
                            FansName = gift.from.nickName
                        };

                   fansDayRankDto.ContributionGoddessValue += (long)(gift.content.price * gift.content.count);
                   fansDayRankApplicationService.AddOrUpdate(fansDayRankDto);
               }, null);
        }
    }
}
