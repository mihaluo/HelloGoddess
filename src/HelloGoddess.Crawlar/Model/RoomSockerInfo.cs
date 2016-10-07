namespace HelloGoddess.Crawlar.Model
{
    public class RoomSockerInfo
    {
        public int errno { get; set; }
        public string errmsg { get; set; }
        public RoomSockerInfoDataData data { get; set; }
    }

    public class RoomSockerInfoDataData
    {
        public string appid { get; set; }
        public int rid { get; set; }
        public string sign { get; set; }
        public string authType { get; set; }
        public long ts { get; set; }
        public string[] chat_addr_list { get; set; }
    }

}