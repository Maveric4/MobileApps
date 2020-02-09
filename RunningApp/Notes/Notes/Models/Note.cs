using System;
using SQLite;

namespace Notes.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string AdditionalNote { get; set; }
        public DateTime Date { get; set; }
        public float Distance { get; set; }
        public float TrainingTime { get; set; }
    }
}
