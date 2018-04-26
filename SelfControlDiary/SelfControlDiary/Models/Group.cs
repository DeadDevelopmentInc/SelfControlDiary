using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public int Course { get; set; }
        public string Name { get; set; }

        public List<Student> Students { get; set; }
        public Faculty Faculty { get; set; }
    }
}
