using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace UploadPhotos.Controllers
{
    public class EmotionController : ApiController
    {
        // GET: api/Emotion
        public async Task<HttpResponseMessage> Get()
        {
            Stream filestream = new FileStream("C:/Users/Ilias/Dropbox/diploma/UploadPhotos_version1_webapi/UploadPhotos/App_Data/smile.jpg", FileMode.Open);


            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://api.projectoxford.ai/emotion/v1.0/recognize");
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "02514d4f80b743718df7675700e46d95");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
            HttpContent content = new StreamContent(filestream);
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/octet-stream");

            var response = await httpClient.PostAsync("https://api.projectoxford.ai/emotion/v1.0/recognize", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            ////sto responseContent tha exoume to apotelesma

            return Request.CreateResponse(HttpStatusCode.OK, responseContent);


        }




    }
}
