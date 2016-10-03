using System;
using HelloGoddess.Infrastructure.Domain.Entities;

namespace HelloGoddess.Core.Domain.Entity
{
    public class DayRank : Entity<int>
    {
        /// <summary>
        /// 女神ID
        /// </summary>
        public int GooddessId { get; set; }

        /// <summary>
        /// 女神名称
        /// </summary>
        public string GoddessName { get; set; }

        /// <summary>
        /// 女神值
        /// </summary>
        public long GoddessValue { get; set; }

        public DateTime DateTime { get; set; }

    }
}