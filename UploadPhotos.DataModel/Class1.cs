using System.Data.Entity;
using UploadPhotos.Models;

namespace UploadPhotos.DataModel
{
    public class PhotoContext : DbContext
    {
        public DbSet<Emotion> Photos { get; set; }
        public DbSet<Facerectangle> FaceLocation { get; set; }
        public DbSet<Scores> Scores { get; set; }
    }
}
