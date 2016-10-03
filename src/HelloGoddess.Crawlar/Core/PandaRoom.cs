﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HelloGoddess.Common.Util;
using HelloGoddess.Crawlar.Core.GiftProcesser;
using HelloGoddess.Crawlar.Model;
using HelloGoddess.Crawlar.Model.Bamboo;
using HelloGoddess.Crawlar.Model.Gift;
using HelloGoddess.Crawlar.Model.Nomal;

namespace HelloGoddess.Crawlar.Core
{
    public class PandaRoom
    {
        private const int IgonreLength = 12;
        private const int MetaLen = 4;
        private const int CheckLen = 4;
        private const int AckLen = 2;
        private static readonly byte[] FirstRsp = {0x00, 0x06, 0x00, 0x06};
        private static readonly byte[] Recvmsg = {0x00, 0x06, 0x00, 0x03};
        private static readonly byte[] KeepALive = {0x00, 0x06, 0x00, 0x00};
        private static readonly byte[] TwoZeroBytes = {0x00, 0x00};
        private static readonly Random Random = new Random();

        private RoomInfo RoomInfo { get; set; }

        public void Connect(long roomId)
        {
            var roomInfoApiUrl = string.Format(PandaConstant.RoomInfoApi, roomId);
            var roomInfo = HttpHelper.DoGet<RoomInfo>(roomInfoApiUrl);
            if ((roomInfo == null) || (roomInfo.errno != 0))
                throw new Exception("get roominfo error");
            RoomInfo = roomInfo;
        }

        public void Execute()
        {
            string rid = RoomInfo.data.rid.ToString(),
                appid = RoomInfo.data.appid,
                k = "1",
                t = "300",
                ts = RoomInfo.data.ts.ToString(),
                sign = RoomInfo.data.sign,
                authType = RoomInfo.data.authType;

            var msg = "u:" + rid + "@" + appid + "\n" +
                      "k:" + k + "\n" +
                      "t:" + t + "\n" +
                      "ts:" + ts + "\n" +
                      "sign:" + sign + "\n" +
                      "authtype:" + authType;

            byte[] bytes = {0x00, 0x06, 0x00, 0x02, 0x00, (byte) msg.Length};
            var msgBytes = Encoding.UTF8.GetBytes(msg);
            var rstBytes = bytes.JoinBytes(msgBytes);
            var chatAddr = RoomInfo.data.chat_addr_list[0];
            var strings = chatAddr.Split(':');
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var socketAsyncEventArgs = new SocketAsyncEventArgs();

            var ipAddress = IPAddress.Parse(strings[0]);
            var remoteEndPoint = new IPEndPoint(ipAddress, int.Parse(strings[1]));
            socketAsyncEventArgs.RemoteEndPoint = remoteEndPoint;
            socketAsyncEventArgs.Completed += (sender, args) =>
            {
                var innerSocket = sender as Socket;
                if (innerSocket == null) return;

                innerSocket.Send(rstBytes);

                var checkBytes = innerSocket.ReceiveWithBuffer(CheckLen);

                if (checkBytes.ToString() == FirstRsp.ToString())
                {
                    Task.Factory.StartNew(() =>
                    {
                        while (true)
                        {
                            var sleepTime = Random.Next(4000, 6000);
                            Thread.Sleep(sleepTime);
                            innerSocket.Send(KeepALive);
                        }
                    });

                    var ackBytes = innerSocket.ReceiveWithBuffer(AckLen);
                    var recvLen = BitConverter.ToInt32(TwoZeroBytes.JoinBytes(ackBytes).BytesOrderByDescending(), 0);
                    var receiveWithBuffer = innerSocket.ReceiveWithBuffer(recvLen);
                    //string s1 = Encoding.UTF8.GetString(receiveWithBuffer);

                    var checkMsg = Encoding.UTF8.GetString(Recvmsg);
                    while (true)
                    {
                        var recvMsg = innerSocket.ReceiveWithBuffer(CheckLen);
                        var s2 = Encoding.UTF8.GetString(recvMsg);
                        var checkResult = s2 == checkMsg;
                        if (checkResult)
                            ReceiveMsg(innerSocket);
                    }
                }
            };
            Socket.ConnectAsync(SocketType.Stream, ProtocolType.Tcp, socketAsyncEventArgs);

            /* socket.Connect(strings[0], int.Parse(strings[1]));
             socket.Send(rstBytes);

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
                 int recvLen = BitConverter.ToInt32(TwoZeroBytes.JoinBytes(ackBytes).BytesOrderByDescending(), 0);
                 var receiveWithBuffer = socket.ReceiveWithBuffer(recvLen);
                 //string s1 = Encoding.UTF8.GetString(receiveWithBuffer);

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
                 }
             }*/
        }


        public static void ReceiveMsg(Socket socket)
        {
            var secondAckBytes = socket.ReceiveWithBuffer(AckLen);
            var joinBytes = TwoZeroBytes.JoinBytes(secondAckBytes).BytesOrderByDescending();
            var revcLen = BitConverter.ToInt32(joinBytes, 0);
            var revcMsg = socket.ReceiveWithBuffer(revcLen); // ack 0
            var metaBytes = socket.ReceiveWithBuffer(MetaLen);
            var totalLen = BitConverter.ToInt32(metaBytes.BytesOrderByDescending(), 0);
            AnalyseMsg(socket, totalLen);
        }

        public static void AnalyseMsg(Socket socket, int totalLength)
        {
            while (totalLength > 0)
            {
                socket.ReceiveWithBuffer(IgonreLength);
                var bytes = socket.ReceiveWithBuffer(MetaLen);
                var recvLen = BitConverter.ToInt32(bytes.BytesOrderByDescending(), 0);
                int length;
                var recvMsg = socket.ReceiveWithBuffer(recvLen, out length);
                while (recvLen > recvMsg.Length)
                {
                    var msgBytes = socket.ReceiveWithBuffer(recvLen - recvMsg.Length);
                    recvMsg = recvMsg.JoinBytes(msgBytes);
                }
                var flag = ProcessMsg(recvMsg);
                totalLength = totalLength - IgonreLength - MetaLen - recvLen;
                if (!flag)
                {
                    //出错，跳过本次消息处理
                    var receiveWithBuffer = socket.ReceiveWithBuffer(totalLength - recvMsg.Length + 1);
                    var processMsgResult = ProcessMsg(recvMsg.JoinBytes(receiveWithBuffer));
                    Console.WriteLine("Result:{0},{1}", processMsgResult, Encoding.UTF8.GetString(receiveWithBuffer));
                    break;
                }
            }
        }

        public static bool ProcessMsg(byte[] msgBytes)
        {
            var msg = Encoding.UTF8.GetString(msgBytes);
            try
            {
                var pandaMessage = msg.ToObj<PandaMessage>();
                var json = pandaMessage.data.ToJson();
                switch (pandaMessage.type)
                {
                    case PandaMessageType.Audience:
                        var audience = json.ToObj<Audience>();
                        Console.WriteLine($"---观看人数{audience.content}---");
                        break;
                    case PandaMessageType.Bamboo:
                        var bamboo = json.ToObj<Bamboo>();
                        Console.WriteLine($"{bamboo.from.nickName}送给主播：{bamboo.content}竹子");
                        break;
                    case PandaMessageType.Gift:
                        var gift = json.ToObj<Gift>();
                        var price = gift.content.price*gift.content.count;
                        Console.WriteLine(
                            $"{gift.from.nickName}送给主播：{gift.content.name} {price}女神票，连击：{gift.content.combo}");
                        IGiftProcesser giftProcesser = new DayRankProcesser();
                        giftProcesser.Process(gift);
                        break;
                    case PandaMessageType.Nomal:
                        var nomal = json.ToObj<Nomal>();
                        Console.WriteLine($"{nomal.from.nickName}：{nomal.content}");
                        break;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(msg);
                Console.WriteLine(exception);
                return false;
            }
            return true;
        }
    }
}