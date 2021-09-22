using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Client
{
    class RequestModel
    {
        private string url = null;
        public RequestModel(string hostUrl)
        {
            url = hostUrl + "api/exchangedata";
        }
        public IEnumerable<MessageModel> GetRequest()
        {
            string json;
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }
            response.Close();
            IEnumerable<MessageModel> messages = JsonSerializer.Deserialize<List<MessageModel>>(json);
            return messages;
        }
        public async void PostRequest(MessageModel model)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            string json = JsonSerializer.Serialize(model);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(json);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = await request.GetResponseAsync();
            response.Close();
        }
    }
}
