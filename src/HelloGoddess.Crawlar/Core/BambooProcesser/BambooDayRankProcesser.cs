using HelloGoddess.Common.Util;
using HelloGoddess.Core.Application;
using HelloGoddess.Core.Domain.Entity;
using HelloGoddess.Crawlar.Model.Bamboo;

namespace HelloGoddess.Crawlar.Core.BambooProcesser
{
    public class BambooDayRankProcesser : IBambooProcesser
    {
        static readonly BambooApplicationService BambooApplicationService = new BambooApplicationService();
        public void Process(Bamboo bamboo)
        {

            if (!Dict.GoddessNameDict.ContainsKey(bamboo.to.toroom))
            {
                return;
            }
            long timeStamp = TimeStampHelper.GetTodayTimeStampSeconds();
            string roomId = bamboo.to.toroom;
            string goddessName = Dict.GoddessNameDict[roomId];
            string fansId = bamboo.from.rid;
            long currentBambooValue = long.Parse(bamboo.content);
            var bambooDayRankDto = BambooApplicationService.GetBambooDayRankDtoByRoomId(fansId, roomId, timeStamp) ??
                                   new BambooDayRankDto
                                   {
                                       TimeStamp = timeStamp,
                                       FansId = fansId,
                                       FansName = bamboo.from.nickName,
                                       RoomId = roomId,
                                       GoddessName = goddessName,
                                   };
            bambooDayRankDto.BambooValue += currentBambooValue;
            BambooApplicationService.AddOrUpdate(bambooDayRankDto);
        }
    }
}