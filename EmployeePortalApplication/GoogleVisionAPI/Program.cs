using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace GoogleVisionAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = String.Format("https://vision.googleapis.com/v1/images:annotate?fields=responses(error%2CsafeSearchAnnotation%2CwebDetection)&key=AIzaSyDXDoLooB6HezLF8dV3kD9kg5xUTJLiaAM");
            string response = CallVisionAPI(url);
            VisionAPIResponse apiResponse = DeserializeResponse(response);
        }

        static string CallVisionAPI(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            VisionAPIRequest request2 = BuildPayload();
            byte[] data = Encoding.ASCII.GetBytes(JObject.FromObject(request2).ToString());

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }

        static VisionAPIRequest BuildPayload()
        {
            Feature webDetect = new Feature { type = "WEB_DETECTION" };
            Feature safeDetect = new Feature { type = "SAFE_SEARCH_DETECTION" };
            Image image = new Image { content = Resource1.ResourceManager.GetString("base64") };

            Request request = new Request
            {
                features = new Feature[] { webDetect, safeDetect },
                image = image
            };

            VisionAPIRequest apiRequest = new VisionAPIRequest();
            apiRequest.requests = new Request[] { request };

            return apiRequest;
        }

        static VisionAPIResponse DeserializeResponse(string response)
        {
            VisionAPIResponse apiResponse = JsonConvert.DeserializeObject<VisionAPIResponse>(response);
            return apiResponse;
        }
    }
}
