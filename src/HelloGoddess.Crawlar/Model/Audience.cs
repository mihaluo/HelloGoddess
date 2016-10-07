namespace HelloGoddess.Crawlar.Model
{
    public class Audience
    {
        public From from { get; set; }
        public To to { get; set; }
        /// <summary>
        /// 人气
        /// </summary>
        public long content { get; set; }
    }

    public class From
    {
        public string rid { get; set; }
    }

    public class To
    {
        /// <summary>
        /// 房间号
        /// </summary>
        public string toroom { get; set; }
    }

}