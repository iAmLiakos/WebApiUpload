using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UploadPhotos.Models
{
    public class PhotoContext : DbContext
    {

        //public PhotoContext()
        //{
        //    Database.SetInitializer<PhotoContext>(new DropCreateDatabaseAlways());

        //}

        public DbSet<Emotion> Photos { get; set; }
        public DbSet<Facerectangle> FaceLocation { get; set; }
        public DbSet<Scores> Scores { get; set; }

    //    public void Seed(PhotoContext context)
    //    {

    //    }

    //    public class DropCreateDatabaseAlways : DropCreateDatabaseAlways<PhotoContext>
    //    {
    //        protected override void Seed(PhotoContext context)
    //        {
    //            context.Seed(context);
    //            base.Seed(context);
    //        }
    //    }
    }
}