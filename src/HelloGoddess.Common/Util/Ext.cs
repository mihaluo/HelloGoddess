using System;
using System.Linq;
using System.Net.Sockets;
using AutoMapper;
using Newtonsoft.Json;

namespace HelloGoddess.Common.Util
{
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
        public static byte[] JoinBytes(this byte[] bytes1, byte[] bytes2)
        {
            byte[] resultBytes = new byte[bytes1.Length + bytes2.Length];
            bytes1.CopyTo(resultBytes, 0);
            bytes2.CopyTo(resultBytes, bytes1.Length);
            return resultBytes;
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

        public static T Map<T>(this object source)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                cfg => cfg.CreateMap(source.GetType(), typeof(T)
                ));

            IMapper mapper = mapperConfiguration.CreateMapper();

            return mapper.Map<T>(source);
        }

        public static T ToObj<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception exception)
            {

            }
            return default(T);
        }
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

    }
}