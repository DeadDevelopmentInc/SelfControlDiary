using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class StandardControlsViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Student { get; set; }
        public DateTime Date { get; set; }
    }
}
