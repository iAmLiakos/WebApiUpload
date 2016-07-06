using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

[assembly: OwinStartup(typeof(UploadPhotos.Startup))]

namespace UploadPhotos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var db = new Models.PhotoContext();
            ConfigureAuth(app);
        }

       
    }

}
