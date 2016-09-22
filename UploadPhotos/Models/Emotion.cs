namespace UploadPhotos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Emotion")]
    public partial class Emotion
    {
        public int Id { get; set; }

        public int FaceId { get; set; }

        public int ScoreId { get; set; }

        public int LocationId { get; set; }

        public virtual Facerectangle Facerectangle { get; set; }

        public virtual Location Location { get; set; }

        public virtual Score Scores { get; set; }
    }
}
