namespace HelloGoddess.Crawlar.Model.Nomal
{
    public class Nomal
    {
        public From from { get; set; }
        public To to { get; set; }
        public string content { get; set; }
    }

    public class From
    {
        public string __plat { get; set; }
        public int identity { get; set; }
        public string level { get; set; }
        public string msgcolor { get; set; }
        public string nickName { get; set; }
        public string rid { get; set; }
        public string sp_identity { get; set; }
        public string userName { get; set; }
    }

    public class To
    {
        public string toroom { get; set; }
    }

}