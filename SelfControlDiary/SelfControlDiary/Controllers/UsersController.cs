using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfControlDiary.Models;

namespace SelfControlDiary.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {

        UserManager<User> _userManager;
        DiaryContext db;

        public UsersController(UserManager<User> userManager, DiaryContext context)
        {
            _userManager = userManager;
            db = context;
        }

        public async Task<IActionResult> Index(string lastName, int? group, int? faculty, int? course, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {

            int pageSize = 10;
            List<UsersStudentViewModel> students = new List<UsersStudentViewModel>();
            var users = await _userManager.GetUsersInRoleAsync("user");
            foreach (var user in users)
            {
                Student student = await db.Students.Include("Group").FirstOrDefaultAsync(t => t.Id == user.StudentId);
                students.Add(new UsersStudentViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    BirthDate = student.BirthDate,
                    FirstName = student.FirstName,
                    Group = student.Group.Name,
                    LastName = student.LastName,
                    Sex = student.Sex,
                    FacultyId = student.Group.FacultyId,
                    Course = student.Group.Course,
                    GroupId = student.Group.Id
                });
            }
            IQueryable<UsersStudentViewModel> filterList = students.AsQueryable();
            if (lastName != null)
            {
                filterList = filterList.Where(p => p.LastName == lastName);
            }
            if (group != null && group != -1)
            {
                filterList = filterList.Where(p => p.GroupId == group);
            }
            if (faculty != null && faculty != -1)
            {
                filterList = filterList.Where(p => p.FacultyId == faculty);
            }
            if (course != null)
            {
                filterList = filterList.Where(p => p.Course == course);
            }
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    {
                        filterList = filterList.OrderByDescending(s => s.LastName);
                        break;
                    }
                case SortState.NameAsc:
                    {
                        filterList = filterList.OrderBy(s => s.LastName);
                        break;
                    }
                case SortState.GroupAsc:
                    {
                        filterList = filterList.OrderBy(s => s.Group);
                        break;
                    }
                case SortState.GroupDesc:
                    {
                        filterList = filterList.OrderByDescending(s => s.Group);
                        break;
                    }
                default:
                    {
                        filterList = filterList.OrderBy(s => s.LastName);
                        break;
                    }
            }
            var count = filterList.Count();
            var items = filterList.Skip((page - 1) * pageSize).
                Take(pageSize).ToList();
            UsersListViewModel model = new UsersListViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(lastName, db.Groups.ToList(), group, db.Faculties.ToList(), faculty, course),
                Students = items
            };
            return View(model);
        }

        public IActionResult FilterGroups(int id)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            list.Add("Все", -1);
            if (id != -1)
            {
                foreach (var item in db.Groups.Where(p => p.FacultyId == id))
                {
                    list.Add(item.Name, item.Id);
                }
            }
            else
            {
                foreach (var item in db.Groups)
                {
                    list.Add(item.Name, item.Id);
                }
            }
            RegisterChangeFacultyViewModel model = new RegisterChangeFacultyViewModel
            {
                Groups = list
            };
            return PartialView(model);
        }

        public IActionResult CreateTeacher() => View();

        public async Task<IActionResult> AdminsList()
        {
            var users = await _userManager.GetUsersInRoleAsync("admin");
            return View(users);
        }

        public IActionResult CreateStudent()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach (var item in db.Groups)
            {
                list.Add(item.Name, item.Id);
            }
            ViewBag.Groups = list;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateUserStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student
                {
                    GroupId = model.GroupId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Sex = model.Sex,
                    BirthDate = model.BirthDate
                };
                await db.Students.AddAsync(student);
                await db.SaveChangesAsync();
                User user = new User { Email = model.Email, UserName = model.Email, StudentId = student.Id };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var res = await _userManager.ConfirmEmailAsync(user, code);
                    await _userManager.AddToRolesAsync(user, new List<string> { "user" });
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Students.Remove(student);
                    await db.SaveChangesAsync();
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach (var item in db.Groups)
            {
                list.Add(item.Name, item.Id);
            }
            ViewBag.Groups = list;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateUserTeacherViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, StudentId = null };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var res = await _userManager.ConfirmEmailAsync(user, code);
                    await _userManager.AddToRolesAsync(user, new List<string> { "admin" });
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditStudent(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            Student student = await db.Students.FirstOrDefaultAsync(t => t.Id == user.StudentId);
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach (var item in db.Groups)
            {
                list.Add(item.Name, item.Id);
            }
            EditUserStudentViewModel model = new EditUserStudentViewModel
            {
                Id = user.Id,
                Email = user.Email,
                GroupId = student.GroupId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Sex = student.Sex,
                BirthDate = student.BirthDate,
                Groups = list
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(EditUserStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    Student student = await db.Students.FirstOrDefaultAsync(t => t.Id == user.StudentId);
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    student.GroupId = model.GroupId;
                    student.BirthDate = model.BirthDate;
                    student.FirstName = model.FirstName;
                    student.LastName = model.LastName;
                    student.Sex = model.Sex;
                    db.Update(student);
                    await db.SaveChangesAsync();
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                Student student = await db.Students.FirstOrDefaultAsync(t => t.Id == user.StudentId);
                db.Remove(student);
                await db.SaveChangesAsync();
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Action = "Index";
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            ErrorViewModel error = new ErrorViewModel
            {
                RequestId = "Не-не=не! Доступ только для преподавателей!"
            };
            return View("Error", error);
        }
    }
}