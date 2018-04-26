using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.Models
{
    public class DiaryContext : IdentityDbContext<User>
    {
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<IndicatorList> IndicatorLists { get; set; }

        public DiaryContext(DbContextOptions<DiaryContext> options) : base(options)
        {

        }
    }
}
