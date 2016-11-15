using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloGoddess.Crawlar.Model.Gift;
using HelloGoddess.Core.Application;
using HelloGoddess.Core.Dto;
using HelloGoddess.Common.Util;
using HelloGoddess.Common.Redis;
using StackExchange.Redis;

namespace HelloGoddess.Crawlar.Core.GiftProcesser
{
    //[Disable]
    public class FansDayRankProcesser : IGiftProcesser
    {
        private const string LockPrex = "FansDayLock";
        const string PreFansDayRank = "PreFansDayRank";

        private static readonly FansDayRankApplicationService FansDayRankApplicationService = new FansDayRankApplicationService();

        public void Process(Gift gift)
        {

            long timeStamp = TimeStampHelper.GetTodayTimeStampSeconds();
            string roomId = Dict.GoddessNameDict.ContainsKey(gift.to.toroom) ? gift.to.toroom : Dict.MainRoomGoddessRoomIdMap[gift.content.name];
            string goddessName = Dict.GoddessNameDict[roomId];
            double currentGoddessValue = (double)(gift.content.price * gift.content.count);
            string fansKey = gift.from.rid + roomId + timeStamp;



            //RedisLock.SingleLock(LockPrex + fansKey,
            //   () =>
            //   {
            bool keyExists = RedisHelper.KeyExists(fansKey);
            double incrementResult = 0;
            if (keyExists)
            {
                incrementResult = RedisHelper.StringIncrement(fansKey, currentGoddessValue);
                if (incrementResult < 100)
                {
                    return;
                }
            }
            var tempFansDayRankDto = new FansDayRankDto
            {
                TimeStamp = timeStamp,
                GoddessRoomId = roomId,
                GoddessName = goddessName,
                FansId = gift.from.rid,
                FansName = gift.from.nickName
            };
            var preFansDayRankKey = PreFansDayRank + roomId;
            var preFansDayRankDto = RedisHelper.Get<FansDayRankDto>(preFansDayRankKey);
            if (preFansDayRankDto == null)
            {
                RedisHelper.Set(preFansDayRankKey, tempFansDayRankDto);
                RedisHelper.StringIncrement(fansKey, currentGoddessValue);
                return;
            }

            string preFansKey = preFansDayRankDto.FansId + preFansDayRankDto.GoddessRoomId + preFansDayRankDto.TimeStamp;

            if (preFansKey == fansKey && incrementResult < 100) return;

            var preGoddessValue = (double)RedisHelper.StringGet(preFansKey);

            var fansDayRankDto = FansDayRankApplicationService.GetFansDayRankDtoByRoomId(
                                 preFansDayRankDto.FansId, preFansDayRankDto.GoddessRoomId, preFansDayRankDto.TimeStamp) ??
                                 preFansDayRankDto;

            fansDayRankDto.ContributionGoddessValue += (long)preGoddessValue;
            FansDayRankApplicationService.AddOrUpdate(fansDayRankDto);

            //cleare predata
            RedisHelper.Remove(preFansDayRankKey);
            RedisHelper.Remove(preFansKey);

            if (preFansKey != fansKey)
            {
                //add new predata
                RedisHelper.Set(preFansDayRankKey, tempFansDayRankDto);
                RedisHelper.StringIncrement(fansKey, currentGoddessValue);

            }



            //}, null);
        }

    }
}
