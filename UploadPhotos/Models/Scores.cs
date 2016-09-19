namespace UploadPhotos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public partial class Scores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Scores()
        {
            Emotion = new HashSet<Emotion>();
        }

        [Key]
        public int scoreID { get; set; }

        public double? anger { get; set; }

        public double? contempt { get; set; }

        public double? disgust { get; set; }

        public double? fear { get; set; }

        public double? happiness { get; set; }

        public double? neutral { get; set; }

        public double? sadness { get; set; }

        public double? surprise { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Emotion> Emotion { get; set; }
    }
}
