using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class Note
    {
        public string Filename { get; set; }
        public string AdditionalNote { get; set; }
        public DateTime Date { get; set; }
        public float Distance { get; set; }
        public float TrainingTime { get; set; }
    }
}
