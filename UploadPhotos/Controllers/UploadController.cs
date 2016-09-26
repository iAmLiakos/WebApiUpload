using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using UploadPhotos.Models;



namespace UploadApplication.Controllers
{
    
    public class UploadController : ApiController
    {
        //[AllowAnonymous]
        [Authorize]
        public async Task<HttpResponseMessage> Post()
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                //if (Request.Content.ReadAsStringAsync().Result.StartsWith("Username:"))
                //{
                    //var request = await Request.Content.ReadAsStringAsync();
                    //ReadAsStringAsync();
                    //var reque = JsonConvert.SerializeObject(request);
                    //var jsoncredentials = JsonConvert.SerializeObject(request);
                    //allagh "\"Username:foo@foo.grLocation:Ioannina\""

                    //Location newloc = JsonConvert.DeserializeObject<Location>(reque);

                    //add username and location names to put into dbcontext
                    //var username = jsoncredentials.

                    //using (var context = new MyPhotoModel())
                    //{

                    //    context.Locations.Add(newloc);
                    //    context.SaveChanges();

                    //}
                //}
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var user = User.Identity.Name;
            var db = new MyPhotoModel();
            //AspNetUser userLog = new AspNetUser();
            //AspNetUser sdsd = await db.AspNetUsers.FindAsync();
            var userid = db.AspNetUsers.Where(b => b.Email == user).FirstOrDefault();
            AspNetUser userrequested = db.AspNetUsers.Where(b => b.Email == user).FirstOrDefault();

            //IEnumerable<string> location = Request.Headers.GetValues("Location");
            //var locationstring = location.ToString();

            IEnumerable < string > headerValues;
            var location = string.Empty;
            var keyFound = Request.Headers.TryGetValues("Location", out headerValues);
            if (keyFound)
            {
                location = headerValues.FirstOrDefault();
            }
            Location loc = new Location(location);
            //loc.AspNetUser = userrequested;
            loc.AspNetUsersId = userrequested.Id;
            loc.Name = location;
            // load data to save           
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data");
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);

            List<string> files = new List<string>();

            //list for accepted file types
            List<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };

            try
            {
                // Read all contents of multipart message into CustomMultipartFormDataStreamProvider.
                //Request.Content.LoadIntoBufferAsync().Wait();
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData file in provider.FileData)
                {

                    var ext = file.LocalFileName.Substring(file.LocalFileName.LastIndexOf('.'));
                    var extension = ext.ToLower();

                    if (AllowedFileExtensions.Contains(extension))
                    {

                        //files.Add(Path.GetFileName(file.LocalFileName));

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
                                                                      
                        // execute the request prosthiki
                        IRestResponse response = client.Execute(request);
                        var responseContent = response.Content; // raw content as string
                        var responseStr = responseContent.Replace(@"\", string.Empty).Trim(new char[] { '\"' });
                        //Deserialize
                        var emotionObject = JsonConvert.DeserializeObject<List<Emotion>>(responseStr);
                        
                        //var scoresObject = emotionObject[0].scores;
                        //Debug.WriteLine(emotionObject[0].scores);

                        //Adding results to my database - entityframework
                        //using (var context = new MyPhotoModel())
                        using (db)
                        {
                            foreach (var eobject in emotionObject)
                            {
                                //eobject.LocationId = loc.Id;
                                //eobject.FaceId = eobject.Facerectangle.Id;
                                //eobject.ScoreId = eobject.Scores.Id;
                                eobject.Location = loc;  
                                //db.Locations.Add(loc);
                                db.Emotions.Add(eobject);
                                //db.AspNetUsers.Add(userrequested);
                                db.ChangeTracker.DetectChanges();
                                db.SaveChanges();
                                //context.Locations.Add(loc);
                                //context.Emotions.Add(eobject);
                                //context.AspNetUsers.Add(userrequested);
                                //context.SaveChanges();

                            }

                        }


                        //Apothikeush tou apotelesmatos se txt arxeio
                        TextWriter write = new StreamWriter("C:/Users/Ilias/Documents/GitHub/WebApiUpload/UploadPhotos/App_Data/result.txt");
                        write.WriteLine(responseContent);                        
                        write.Close();

                        /*
                        //playing with json - good output
                        StreamReader sr = new StreamReader("C:/Users/Ilias/Documents/GitHub/WebApiUpload/UploadPhotos/App_Data/result.txt");
                        //StreamReader sr = new StreamReader(responseContent);
                        JsonTextReader jtr = new JsonTextReader(sr);
                        var serializer = new JsonSerializer();
                        //var jsonObject = serializer.Deserialize(jtr);
                        //Emotion newPhoto = (Emotion)serializer.Deserialize(jtr);
                        */
                        /*
                        //dhmiourgia object apo json
                        StreamReader re = new StreamReader("C:/Users/Ilias/Documents/GitHub/WebApiUpload/UploadPhotos/App_Data/result.txt");
                        JsonTextReader reader = new JsonTextReader(re);
                        JsonSerializer js = new JsonSerializer();                                         
                        Emotion newPhoto = (Emotion)js.Deserialize(reader);
                        */

                        //sto responseContent tha exoume to apotelesma se Json
                        //return Request.CreateResponse(HttpStatusCode.Accepted, responseContent);
                        return Request.CreateResponse(HttpStatusCode.OK, responseContent);

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