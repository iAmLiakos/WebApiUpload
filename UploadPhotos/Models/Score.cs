namespace UploadPhotos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Score")]
    public partial class Score
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Score()
        {
            Emotions = new HashSet<Emotion>();
        }

        public int Id { get; set; }

        public double? anger { get; set; }

        public double? contempt { get; set; }

        public double? disgust { get; set; }

        public double? fear { get; set; }

        public double? happiness { get; set; }

        public double? neutral { get; set; }

        public double? sadness { get; set; }

        public double? surprise { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Emotion> Emotions { get; set; }
    }
}
