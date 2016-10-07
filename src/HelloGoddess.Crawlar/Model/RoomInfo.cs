using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloGoddess.Crawlar.Model
{
    public class RoomInfo
    {
        public int errno { get; set; }
        public string errmsg { get; set; }
        public Data data { get; set; }
        public string authseq { get; set; }
    }

    public class Data
    {
        public Item[] items { get; set; }
        public string total { get; set; }
    }

    public class Item
    {
        public string md5 { get; set; }
        public string nickname { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public string roomid { get; set; }
        public string hostid { get; set; }
        public string name { get; set; }
        public string style { get; set; }
        public string _class { get; set; }
        public string classification { get; set; }
        /// <summary>
        /// 在线人数
        /// </summary>
        public string person_num { get; set; }
        /// <summary>
        /// 竹子数
        /// </summary>
        public string bamboos { get; set; }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public string fans { get; set; }
        /// <summary>
        /// 2 为在线 ，3为不在线
        /// </summary>
        public string status { get; set; }
        public string content { get; set; }
        public Pictures pictures { get; set; }
        public string updatetime { get; set; }
        public string reliable { get; set; }
        public string sex { get; set; }
        public string province { get; set; }
        public string url_footprint { get; set; }
        public Se se { get; set; }
        public string display_type { get; set; }
        public string tag { get; set; }
        public string tag_switch { get; set; }
        public string tag_color { get; set; }
    }

    public class Pictures
    {
        public string img { get; set; }
        public string qrcode { get; set; }
    }

    public class Se
    {
        public int prefix { get; set; }
        public int docId { get; set; }
        public int sort0 { get; set; }
    }


}
