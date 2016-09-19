namespace UploadPhotos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Facerectangle")]
    public partial class Facerectangle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Facerectangle()
        {
            Emotion = new HashSet<Emotion>();
        }

        public int Id { get; set; }

        public int? height { get; set; }

        public int? left { get; set; }

        public int? top { get; set; }

        public int? width { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Emotion> Emotion { get; set; }
    }
}
