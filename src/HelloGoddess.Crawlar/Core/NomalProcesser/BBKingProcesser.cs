using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloGoddess.Crawlar.Model.Nomal;
using HelloGoddess.Common.Util;
using HelloGoddess.Core.Application;
using HelloGoddess.Common.Redis;
using HelloGoddess.Core.Dto;

namespace HelloGoddess.Crawlar.Core.NomalProcesser
{
    public class BBKingProcesser : INomalProcesser
    {
        private const string LockPrex = "BBKingLock";

        public void Process(Nomal nomal)
        {
            if (!Dict.GoddessNameDict.ContainsKey(nomal.to.toroom))
            {
                return;
            }
            BBKingApplicationService bbkingApplicationService = new BBKingApplicationService();

            long timeStamp = TimeStampHelper.GetTodayTimeStampSeconds();
            string roomId = nomal.to.toroom;
            string goddessName = Dict.GoddessNameDict[roomId];
            RedisLock.SingleLock(LockPrex + nomal.from.rid + roomId + timeStamp,
             () =>
             {
                 BBKingDayRankDto bbkingDayRankDto = bbkingApplicationService.GetBBKingDayRankDtoByRoomId(nomal.from.rid, roomId, timeStamp) ??
                                new BBKingDayRankDto
                                {
                                    TimeStamp = timeStamp,
                                    GoddessName = goddessName,
                                    RoomId = roomId,
                                    FansId = nomal.from.rid,
                                    FansName = nomal.from.nickName,
                                    Identity = (IdentityType)nomal.from.identity
                                };

                 bbkingDayRankDto.BBTimes += 1;
                 bbkingApplicationService.AddOrUpdate(bbkingDayRankDto);
             }, null);
        }

    }
}
