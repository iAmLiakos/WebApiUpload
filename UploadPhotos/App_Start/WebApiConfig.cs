using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using UploadPhotos.Models;
using UploadPhotos.Controllers;

namespace UploadPhotos
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //setting up odata model 
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Emotion>("MyOdata");
            builder.EntitySet<Facerectangle>("Facerectangles");
            builder.EntitySet<Scores>("Scores");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
                  
        }
    }
}
