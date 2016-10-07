using HelloGoddess.Core.Dto;
using HelloGoddess.Infrastructure.Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloGoddess.Core.Domain.Entity
{
    public class AudienceRankDto : IntKeyDto
    {
        /// <summary>
        /// 女神ID
        /// </summary>
        public int GooddessId { get; set; }

        /// <summary>
        /// RoomId
        /// </summary>
        public string RoomId { get; set; }

        /// <summary>
        /// 女神名称
        /// </summary>
        public string GoddessName { get; set; }

        /// <summary>
        ///人气
        /// </summary>
        public long Audience { get; set; }

        /// <summary>
        /// 当前日期
        /// </summary>
        public long CurrentDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateDate { get; set; }
    }
}
