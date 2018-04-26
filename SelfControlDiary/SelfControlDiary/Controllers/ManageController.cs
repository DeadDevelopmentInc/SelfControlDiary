using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SelfControlDiary.Models;

namespace SelfControlDiary.Controllers
{
    [Authorize]
    public class LKController : Controller
    {
        DiaryContext db;
        UserManager<User> userManager;

        public LKController(DiaryContext context, UserManager<User> _userManager)
        {
            userManager = _userManager;
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            Student student = await db.Students.Include("Group").FirstOrDefaultAsync(t => t.Id == user.StudentId);
            IndexLKViewModel model = new IndexLKViewModel
            {
                StudentId = (int)user.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                Group = student.Group.Name,
                Sex = student.Sex
            };
            return View(model);
        }
    }
}