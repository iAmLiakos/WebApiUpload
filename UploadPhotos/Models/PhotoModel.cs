namespace UploadPhotos.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PhotoModel : DbContext
    {

        

        public PhotoModel()
            : base("name=PhotoModel1")
        {
            Database.SetInitializer<PhotoModel>(null);
        }

        public virtual DbSet<Emotion> Emotion { get; set; }
        public virtual DbSet<Facerectangle> Facerectangle { get; set; }
        public virtual DbSet<Scores> Scores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
