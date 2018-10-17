using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Utils
{
    public class SerializerHelper
    {
        public static byte[] Serialize<T>(T data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, data);
                byte[] raw = memoryStream.ToArray();                
                return raw;
            }
        }

        public static T Deserialize<T>(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                return Serializer.Deserialize<T>(memoryStream);
            }                
        }
    }
}