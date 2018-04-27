using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class StandardsControl
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int Semestr { get; set; }
        public int Run { get; set; }
        public int Incline { get; set; }
        public int Bending { get; set; }
        public int? Pulling { get; set; }
        public int Squatting { get; set; }
        public int? Press { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Student Student { get; set; }
    }
}
