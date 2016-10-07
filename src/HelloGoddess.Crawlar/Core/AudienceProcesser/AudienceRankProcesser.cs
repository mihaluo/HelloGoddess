using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloGoddess.Crawlar.Model;
using HelloGoddess.Core.Application;
using HelloGoddess.Common.Util;

namespace HelloGoddess.Crawlar.Core.AudienceProcesser
{
    public class AudienceRankProcesser : IAudienceProcesser
    {
        public void Process(Audience audience)
        {
            var app = new AudienceRankApplicationService();

            string goddessName = Dict.GoddessNameDict.ContainsKey(audience.to.toroom) ? Dict.GoddessNameDict[audience.to.toroom] : "主房间";
            var audienceRankDto = new HelloGoddess.Core.Domain.Entity.AudienceRankDto
            {
                RoomId = audience.to.toroom,
                GooddessId = 0,
                GoddessName = goddessName,
                Audience = audience.content,
                CreateDate = TimeStampHelper.GetCurrentDateTimeStampSeconds(),
                CurrentDate = TimeStampHelper.GetTodayTimeStampSeconds()
            };
            app.Add(audienceRankDto);
        }
    }
}
