using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Group> Groups { get; set; }
    }
}
