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
        const string PreDayRank = "PreDayRank";

        public void Process(Gift gift)
        {

            DayRankApplicationService dayRankApplicationService = new DayRankApplicationService();

            long timeStamp = TimeStampHelper.GetTodayTimeStampSeconds();
            string roomId = Dict.GoddessNameDict.ContainsKey(gift.to.toroom) ? gift.to.toroom : Dict.MainRoomGoddessRoomIdMap[gift.content.name];
            string goddessName = Dict.GoddessNameDict[roomId];
            double currentGoddessValue = (double)(gift.content.price * gift.content.count);


            string dayRankKey = roomId + timeStamp;


            //RedisLock.SingleLock(LockPrex + dayRankKey,
            //() =>
            //{
            bool keyExists = RedisHelper.KeyExists(dayRankKey);
            double increment = 0;
            if (keyExists)
            {
                increment = RedisHelper.StringIncrement(dayRankKey, currentGoddessValue);
                if (increment < 100)
                {
                    return;
                }
            }
            var rankDto = new DayRankDto
            {
                TimeStamp = timeStamp,
                GoddessName = goddessName,
                RoomId = roomId
            };
            var preDayRank = RedisHelper.Get<DayRankDto>(PreDayRank);
            if (preDayRank == null)
            {
                RedisHelper.Set(PreDayRank, rankDto);
                RedisHelper.StringIncrement(dayRankKey, currentGoddessValue);
                return;
            }

            string preDayRanKey = preDayRank.RoomId + preDayRank.TimeStamp;
            if (preDayRanKey == dayRankKey && increment < 100) return;

            var dayRankDto = dayRankApplicationService.GetDayRankDtoByRoomIdAndDate
                             (preDayRank.RoomId, preDayRank.TimeStamp) ??
                             preDayRank;

            var preGoddessValue = (double)RedisHelper.StringGet(preDayRanKey);
            dayRankDto.GoddessValue += (long)preGoddessValue;
            dayRankApplicationService.AddOrUpdate(dayRankDto);

            //cleare predata
            RedisHelper.Remove(PreDayRank);
            RedisHelper.Remove(preDayRanKey);
            if (preDayRanKey != dayRankKey)
            {
                //add new predata
                RedisHelper.Set(PreDayRank, rankDto);
                RedisHelper.StringIncrement(dayRankKey, currentGoddessValue);

            }

            //}, null);
        }
    }
}