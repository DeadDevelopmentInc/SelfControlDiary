using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class User : IdentityUser
    {
        public int? StudentId { get; set; }
    }
}
