namespace UploadPhotos.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyPhotoModel : DbContext
    {
        public MyPhotoModel()
            : base("name=Model2")
        {
        }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Emotion> Emotions { get; set; }
        public virtual DbSet<Facerectangle> Facerectangles { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Locations)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.AspNetUsersId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Facerectangle>()
                .HasMany(e => e.Emotions)
                .WithRequired(e => e.Facerectangle)
                .HasForeignKey(e => e.FaceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Emotions)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Score>()
                .HasMany(e => e.Emotions)
                .WithRequired(e => e.Scores)
                .WillCascadeOnDelete(false);
        }
    }
}
