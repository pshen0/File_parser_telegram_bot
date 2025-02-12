using System;
using System.Text;
using System.Text.Json;

namespace Class_Library
{
    public class JsonProcessing
    {
        /// <summary>
        /// The method uses serializer to read the data.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public List<ElectrocarPower> Read(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                List<ElectrocarPower> res = JsonSerializer.Deserialize<List<ElectrocarPower>>(json);
                return res;
            }
        }
        /// <summary>
        /// The method uses serializer to write the data.
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public Stream Write(List<ElectrocarPower> Data)
        {
            var serializer = new JsonSerializerOptions { WriteIndented = true };
            serializer.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            string json = JsonSerializer.Serialize(Data, serializer);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }
    }
}

