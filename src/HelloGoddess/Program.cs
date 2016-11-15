using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HelloGoddess.Common.Redis;
using HelloGoddess.Common.Util;
using HelloGoddess.Core.Application;
using HelloGoddess.Core.Domain.Entity;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;

namespace HelloGoddess
{
    public class Program
    {
        private static readonly byte[] FirstRsp = new byte[] { 0x00, 0x06, 0x00, 0x06 };
        private static readonly byte[] Recvmsg = new byte[] { 0x00, 0x06, 0x00, 0x03 };
        private static readonly byte[] KeepALive = new byte[] { 0x00, 0x06, 0x00, 0x00 };
        private static readonly byte[] TwoZeroBytes = new byte[] { 0x00, 0x00 };
        const int IgonreLength = 12;
        const int MetaLen = 4;
        const int CheckLen = 4;
        const int AckLen = 2;
        private static readonly Random Random = new Random();

        public class ChatInfo
        {
            public string errno { get; set; }
            public string errmsg { get; set; }
            public Data data { get; set; }
            public string authseq { get; set; }
        }

        public class Data
        {
            public string rid { get; set; }
            public string roomid { get; set; }
            public string ts { get; set; }
            public string sign { get; set; }
        }

        private static long value;

        public static void Main(string[] args)
        {
            string token = Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n").Substring(0,11);
            int le = token.Length;
            string tokenapi = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=TOKEN";
            HttpHelper.DoPost<string>(tokenapi,"" );
            const string incrementkey = "incrementKey";
            RedisHelper.Remove(incrementkey);
            for (int i = 0; i < 100; i++)
            {
                var value1 = (double)1;
                RedisHelper.StringIncrement(incrementkey, value1);

            }
            Console.WriteLine(RedisHelper.StringGet(incrementkey));
            RedisHelper.Set("test", new Test
            {
                Name = "asas",
                Total = 1000
            });
            var test = RedisHelper.Get<Test>("test");
            RedisHelper.Remove("test");
            var test1 = RedisHelper.Get<Test>("test");

            Console.WriteLine(value);
            var dns = Dns.GetHostAddressesAsync("www.baidu.com").Result;
            foreach (var ipAddress in dns)
            {
                var ip = ipAddress.ToString();
                Console.WriteLine(ip);
            }
            var bytes2 = Encoding.UTF8.GetBytes("]");
            //{ "errno":"0","errmsg":"","data":{ "rid":"-40494344","roomid":"485118","ts":"1475055608598","sign":"e851d00f67af95feb4b114ce5de6bc88"},"authseq":""}
            HttpClient httpClient = new HttpClient(new MyHttpClienHanlder());
            string pandaTvChatInfoApiUrl =
                "http://static.api.m.panda.tv/index.php?method=chat.chatinfo&roomid=485118&rid=0&__plat=barragetool&__version=1.0.4.1029";
            var result1 = httpClient.GetStringAsync(pandaTvChatInfoApiUrl).Result;
            var chatInfo = JsonConvert.DeserializeObject<ChatInfo>(result1);
            string pandaTvRoomInfo =
                $"http://api.homer.panda.tv/chatroom/getinfo?rid={chatInfo.data.rid}&roomid={chatInfo.data.roomid}&sign={chatInfo.data.sign}&ts={chatInfo.data.ts}&retry=0&__plat=barragetool&__version=1.0.4.1029";
            HttpClient httpClient2 = new HttpClient();
            //var result2 = httpClient2.GetStringAsync(pandaTvRoomInfo).Result;
            //JsonConvert.DeserializeObject<RoomInfo>(result2);
            var bytes1 = BitConverter.GetBytes(16);
            var array = bytes1.OrderBy(b1 => b1).ToArray();
            //TestApplicationService testApplicationService = new TestApplicationService();
            //testApplicationService.Insert();
            string s = httpClient.GetStringAsync("http://riven.panda.tv/chatroom/getinfo?roomid=353622").Result;
            //s = @"{""errno"":0,""errmsg"":"""",""data"":{""appid"":""134702846"",""rid"":-3222703,""sign"":""79098a908f964692824259c4449998dc"",""authType"":""3"",""ts"":1475385387000,""chat_addr_list"":[""123.56.31.103:443"",""101.201.70.99:443""]}}";
            var deserializeObject = JsonConvert.DeserializeObject<RoomInfo>(s);
            string rid = deserializeObject.data.rid.ToString(),
                appid = deserializeObject.data.appid,
                k = "1",
                t = "300",
                ts = deserializeObject.data.ts.ToString(),
                sign = deserializeObject.data.sign,
                authType = deserializeObject.data.authType;

            String msg = "u:" + rid + "@" + appid + "\n" +
             "k:" + k + "\n" +
             "t:" + t + "\n" +
             "ts:" + ts + "\n" +
             "sign:" + sign + "\n" +
             "authtype:" + authType;
            /*msg = "authtype:" + authType + "\n" +
                  "sign:" + sign + "\n" +
                  "t:" + t + "\n" +
              "ts:" + ts + "\n" +
              "u:" + rid + "@" + appid
              ;
*/

            byte[] bytes = new byte[] { 0x00, 0x06, 0x00, 0x02, 0x00, (byte)msg.Length };
            byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
            byte[] rstBytes = JoinBytes(bytes, msgBytes);
            var chatAddr = deserializeObject.data.chat_addr_list[0];
            string[] strings = chatAddr.Split(':');
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(strings[0], int.Parse(strings[1]));
            socket.Send(rstBytes);

            /*  int receiveLength;
              var withBuffer = socket.ReceiveWithBuffer(1024 * 20, out receiveLength);
              var s2 = Encoding.UTF8.GetString(withBuffer, 0, receiveLength);
              socket.Dispose();
  */
            var checkBytes = socket.ReceiveWithBuffer(CheckLen);

            if (checkBytes.ToString() == FirstRsp.ToString())
            {
                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        int sleepTime = Random.Next(4000, 6000);
                        Thread.Sleep(sleepTime);
                        socket.Send(KeepALive);
                    }
                });
                var ackBytes = socket.ReceiveWithBuffer(AckLen);

                int recvLen = BitConverter.ToInt32(JoinBytes(TwoZeroBytes, ackBytes).BytesOrderByDescending(), 0);
                var receiveWithBuffer = socket.ReceiveWithBuffer(recvLen);
                string s1 = Encoding.UTF8.GetString(receiveWithBuffer);

                int intervalTime = 1000;
                var dateTime = DateTime.Now;
                string checkMsg = Encoding.UTF8.GetString(Recvmsg);
                while (true)
                {
                    var recvMsg = socket.ReceiveWithBuffer(CheckLen);
                    string s2 = Encoding.UTF8.GetString(recvMsg);
                    bool checkResult = s2 == checkMsg;
                    if (checkResult)
                    {
                        ReceiveMsg(socket);
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
            byte[] b = new byte[] { 0x00, 0x06, 0x00, 0x02, 0x00, };
            socket.Dispose();
            const string pandatvLoginUrl = "https://u.panda.tv/ajax_login";
            long unixTimeMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            List<KeyValuePair<string, string>> formKeys = new List<KeyValuePair<string, string>>();
            formKeys.Add(new KeyValuePair<string, string>("account", "15110036458"));
            formKeys.Add(new KeyValuePair<string, string>("password", "c73;/+gA669%[i"));
            formKeys.Add(new KeyValuePair<string, string>("_jp", "jQuery19108310728466630561_1474273395789"));
            formKeys.Add(new KeyValuePair<string, string>("__plat", "pc_web"));
            formKeys.Add(new KeyValuePair<string, string>("__guid", "0.4075399580859141000.1463712664865.843"));
            formKeys.Add(new KeyValuePair<string, string>("refer", "http://www.panda.tv/all"));
            //formKeys.Add(new KeyValuePair<string, string>("pdftsrc", @"{""os"":""web"",""sessionId"":""1474273395988-4172209"",""smid"":""18be6ebc-8b75-4f58-9042-31d6302c806b"",""canvas"":""4c99fce1850b49c10115b0bd0ccc8f63"",""h"":900,""ua"":""ec10ee27fbe4ef1e0ab9563187962bf6"",""w"":1600,""lip"":""192.168.1.115""}"));
            formKeys.Add(new KeyValuePair<string, string>("_", unixTimeMilliseconds.ToString()));
            HttpContent httpContent = new FormUrlEncodedContent(formKeys);
            var httpResponseMessage = httpClient.PostAsync(pandatvLoginUrl, httpContent).Result;
            string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
            //List<KeyValuePair<string, string>> formKeys = new List<KeyValuePair<string, string>>();
            //formKeys.Add(new KeyValuePair<string, string>("loginname", "T18618320929"));
            //formKeys.Add(new KeyValuePair<string, string>("password", "123456"));
            //HttpContent httpContent = new FormUrlEncodedContent(formKeys);
            //var httpResponseMessage = httpClient.PostAsync("http://vpiao.dianzipiao.cn/login/index", httpContent).Result;
            //string result = httpClient.GetStringAsync("http://vpiao.dianzipiao.cn/ScenicSpot/ScenicSpotList").Result;
        }

        public static void ReceiveMsg(Socket socket)
        {
            var secondAckBytes = socket.ReceiveWithBuffer(AckLen);
            byte[] joinBytes = JoinBytes(TwoZeroBytes, secondAckBytes).BytesOrderByDescending();
            int revcLen = BitConverter.ToInt32(joinBytes, 0);
            var revcMsg = socket.ReceiveWithBuffer(revcLen);// ack 0
            var metaBytes = socket.ReceiveWithBuffer(MetaLen);
            int totalLen = BitConverter.ToInt32(metaBytes.BytesOrderByDescending(), 0);
            AnalyseMsg(socket, totalLen);
        }

        public static void AnalyseMsg(Socket socket, int totalLength)
        {
            while (totalLength > 0)
            {
                socket.ReceiveWithBuffer(IgonreLength);
                byte[] bytes = socket.ReceiveWithBuffer(MetaLen);
                int recvLen = BitConverter.ToInt32(bytes.BytesOrderByDescending(), 0);
                var recvMsg = socket.ReceiveWithBuffer(recvLen);
                while (recvLen > recvMsg.Length)
                {
                    var msgBytes = socket.ReceiveWithBuffer(recvLen - recvMsg.Length);
                    recvMsg = JoinBytes(recvMsg, msgBytes);
                }
                FormatMsg(recvMsg);
                totalLength = totalLength - IgonreLength - MetaLen - recvLen;
            }

        }

        public static void FormatMsg(byte[] msgBytes)
        {
            var msg = Encoding.UTF8.GetString(msgBytes);
            Console.WriteLine(msg);
        }

        public static byte[] JoinBytes(byte[] bytes1, byte[] bytes2)
        {
            byte[] resultBytes = new byte[bytes1.Length + bytes2.Length];
            bytes1.CopyTo(resultBytes, 0);
            bytes2.CopyTo(resultBytes, bytes1.Length);
            return resultBytes;
        }
    }

    public static class Ext
    {
        public static byte[] BytesOrderBy(this byte[] bytes)
        {
            return bytes.OrderBy(b => b).ToArray();
        }

        public static byte[] BytesOrderByDescending(this byte[] bytes)
        {
            return bytes.OrderByDescending(b => b).ToArray();
        }

        public static byte[] ReceiveWithBuffer(this Socket socket, int length)
        {
            int receiveLength;
            return ReceiveWithBuffer(socket, length, out receiveLength);
        }

        public static byte[] ReceiveWithBuffer(this Socket socket, int length, out int receiveLength)
        {
            byte[] bytes = new byte[length];
            receiveLength = socket.Receive(bytes);
            return bytes;
        }
    }
    public class RoomInfo
    {
        public int errno { get; set; }
        public string errmsg { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string appid { get; set; }
        public int rid { get; set; }
        public string sign { get; set; }
        public string authType { get; set; }
        public long ts { get; set; }
        public string[] chat_addr_list { get; set; }
    }




    public class MyHttpClienHanlder : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //request.Headers.Referrer = new Uri("http://www.panda.tv");
            request.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10240");
            //request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Accept", "text/html, application/xhtml+xml, image/jxr, */*");
            return base.SendAsync(request, cancellationToken);
        }
    }



}
