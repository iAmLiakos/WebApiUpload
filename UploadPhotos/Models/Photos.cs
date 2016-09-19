using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;


namespace UploadPhotos.Models
{
    public class Emotion
    {
        [Key]
        public int EmotionId { get; set; }
        [Required]
        public Facerectangle faceRectangle { get; set; }    
        [Required]   
        public Scores scores { get; set; }
    }

    public class Facerectangle
    {
        [Key]
        public int faceid { get; set; }
        public int height { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
    }

    public class Scores
    {
        
        //public int scoreid { get; set; }
        [Key]
        public float anger { get; set; }
        public float contempt { get; set; }
        public float disgust { get; set; }
        public float fear { get; set; }
        public float happiness { get; set; }
        public float neutral { get; set; }
        public float sadness { get; set; }
        public float surprise { get; set; }
    }


}