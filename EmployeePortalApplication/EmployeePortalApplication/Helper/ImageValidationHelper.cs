using GoogleVisionAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace LGSS.Mentoring.EmployeePortal.Helper
{
    public static class ImageValidationHelper
    {
        public static bool ValidateImage(string base64)
        {
            string url = String.Format("https://vision.googleapis.com/v1/images:annotate?key=AIzaSyDXDoLooB6HezLF8dV3kD9kg5xUTJLiaAM");
            string response = CallVisionAPI(url, base64);
            VisionAPIResponse apiResponse = DeserializeResponse(response);

            // Check if a response exists
            Response responseCheck = apiResponse.responses.SingleOrDefault();

            if (responseCheck == null)
            {
                return false;
            }

            // Check if there are any faces or more than one
            Faceannotation[] responseFaces = responseCheck.faceAnnotations;

            if (responseFaces.Count() > 1 || responseFaces.Count() == 0)
            {
                return false;
            }

            // Check if description, passport and score
            List<Webentity> entities = responseCheck.webDetection.webEntities
                .Where(i => i.description != null
                && i.description.ToLower().Contains("passport")
                && i.score > 0.5)
                .ToList();

            //TODO: Blend checking logic with new logic
            // Check if the entities list is greater than 0
            return entities.Count > 0 ? true : false;


        }

        static string CallVisionAPI(string url, string base64)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            VisionAPIRequest request2 = BuildPayload(base64);
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

        static VisionAPIRequest BuildPayload(string base64)
        {
            Feature webDetect = new Feature { type = "WEB_DETECTION" };
            Feature safeDetect = new Feature { type = "SAFE_SEARCH_DETECTION" };
            Feature labelDetect = new Feature { type = "LABEL_DETECTION" };
            Feature faceDetect = new Feature { type = "FACE_DETECTION" };
            Image image = new Image { content = base64 };

            Request request = new Request
            {
                features = new Feature[] { webDetect, safeDetect, labelDetect, faceDetect },
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