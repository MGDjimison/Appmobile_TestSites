using System;
using System.Threading.Tasks;
using Notes.Data;
using SQLite;

namespace Notes.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public string url { get; set; }
        public DateTime Date { get; set; }
        public string Filename { get; internal set; }
        public string color { get; set; }
        public bool isActive { get; set; }
        public bool isInactive { get { return (!(bool)isActive); } }
    }
}