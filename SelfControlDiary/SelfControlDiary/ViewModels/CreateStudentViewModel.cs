﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class CreateStudentViewModel
    {
        public int GroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Dictionary<string, int> Groups { get; set; }
    }
}
