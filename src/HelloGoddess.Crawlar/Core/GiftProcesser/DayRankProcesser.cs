using System;
using HelloGoddess.Core.Application;
using HelloGoddess.Core.Dto;
using HelloGoddess.Crawlar.Model.Gift;

namespace HelloGoddess.Crawlar.Core.GiftProcesser
{
    public class DayRankProcesser : IGiftProcesser
    {
        public void Process(Gift gift)
        {
            DayRankApplicationService dayRankApplicationService = new DayRankApplicationService();
            var dayRankDto = new DayRankDto
            {
                DateTime = DateTime.Now,
                GoddessName = gift.content.name,
                GoddessValue = (long)(gift.content.price * gift.content.count),
                GooddessId = int.Parse(gift.content.id)
            };
            dayRankApplicationService.AddOrUpdate(dayRankDto);

        }
    }
}