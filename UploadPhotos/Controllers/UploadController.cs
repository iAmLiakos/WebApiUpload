﻿using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;



namespace UploadApplication.Controllers
{
    public class UploadController : ApiController
    {
        public async Task<HttpResponseMessage> Post()
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // load data to save           
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data");
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);

            List<string> files = new List<string>();

            //list for accepted file types
            List<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };

            try
            {
                // Read all contents of multipart message into CustomMultipartFormDataStreamProvider.
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData file in provider.FileData)
                {

                    var ext = file.LocalFileName.Substring(file.LocalFileName.LastIndexOf('.'));
                    var extension = ext.ToLower();

                    if (AllowedFileExtensions.Contains(extension))
                    {

                        files.Add(Path.GetFileName(file.LocalFileName));

                        //DO STUFF WITH PHOTOS/EMOTIONS

                        //HTTP REQUEST to the Emotion API                    
                        //Stream filestream = new FileStream(file.LocalFileName, FileMode.Open);
                                                
                        //xtizw to swma tou request

                        //Using RestSharp
                        var client = new RestClient("https://api.projectoxford.ai/emotion/v1.0/recognize");                        
                        var request = new RestRequest(Method.POST);
                        request.RequestFormat = DataFormat.Json;
                        request.AddHeader("Ocp-Apim-Subscription-Key", "02514d4f80b743718df7675700e46d95");
                        //request.AddParameter("content-type", "application/octet-stream");
                        //request.AddParameter("Host", "api.projectoxford.ai");
                        Byte[] imageBytes;                        
                        //Debugging
                        //File.Open(@"C:/Users/Ilias/Documents/GitHub/WebApiUpload/UploadPhotos/App_Data/smile.jpg", FileMode.Open)
                        using (FileStream fs = new FileStream(file.LocalFileName, FileMode.Open))
                        {
                            imageBytes = new BinaryReader(fs).ReadBytes((int)fs.Length);
                        }
                        request.AddParameter("application/octet-stream", imageBytes, ParameterType.RequestBody);
                                                                      
                        // execute the request
                        IRestResponse response = client.Execute(request);
                        var responseContent = response.Content; // raw content as string
                        
                        /*Old Request and Response
                        var httpClient = new HttpClient();
                        httpClient.BaseAddress = new Uri("https://api.projectoxford.ai/emotion/v1.0/recognize");
                        httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "02514d4f80b743718df7675700e46d95");
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                        HttpContent content = new StreamContent(filestream);
                        content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/octet-stream");
                        //xtizw to response
                        var response = await httpClient.PostAsync("https://api.projectoxford.ai/emotion/v1.0/recognize", content);
                        string responseContent = await response.Content.ReadAsStringAsync();
                        */

                        //Apothikeush tou apotelesmatos se txt arxeio
                        TextWriter write = new StreamWriter("C:/Users/Ilias/Documents/GitHub/WebApiUpload/UploadPhotos/App_Data/result.txt");
                        write.WriteLine(responseContent);                        
                        write.Close();                        
                        
                        //sto responseContent tha exoume to apotelesma se Json
                        return Request.CreateResponse(HttpStatusCode.Accepted, responseContent);

                    }

                    //Wrong type posted in json
                    else return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "Wrong Type");
                }

                // Send OK Response -- debugging

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }

    }

    // Implementing MultipartFormDataStreamProvider to override the filename of File which
    // will be stored on server, or else the default name will be of the format like Body-
    // Part_{GUID}. In the following implementation we simply get the FileName from 
    // ContentDisposition Header of the Request Body.
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
        }
    }




}