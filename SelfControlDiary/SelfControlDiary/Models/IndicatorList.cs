using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class IndicatorList
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Semestr { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int JEL { get; set; }
        public int BaseForce { get; set; }
        public int LeftWrist { get; set; }
        public int RightWrist { get; set; }
        public int Pause { get; set; }
        public int Breath { get; set; }
        public int Exhalation { get; set; }
        public int ChSS { get; set; }
        public int ADS { get; set; }
        public int ADD { get; set; }
        public int Genchi { get; set; }
        public int Shtange { get; set; }
        public int CCC { get; set; }
        public int Stat { get; set; }


        public Student Student { get; set; }
    }
}
