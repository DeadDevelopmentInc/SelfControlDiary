using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelfControlDiary.Models;

namespace SelfControlDiary.Controllers
{
    [Authorize(Roles = "admin")]
    public class FacultiesController : Controller
    {
        DiaryContext db;

        public FacultiesController(DiaryContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Faculties.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFacultyViewModel model)
        {
            int er = 0;
            if (ModelState.IsValid && (er = db.Faculties.Count(p => p.Name == model.Name)) == 0)
            {
                Faculty faculty = new Faculty
                {
                    Name = model.Name
                };
                await db.Faculties.AddAsync(faculty);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("Name", "Факультет с таким именем уже есть");
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Faculty faculty = await db.Faculties.FirstOrDefaultAsync(t => t.Id == id);
                if (faculty == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! В базе данных отсутствует запись с переданным id = " + id
                    };
                    return View("Error", error);
                }
                EditFacultyViewModel model = new EditFacultyViewModel
                {
                    Id = faculty.Id,
                    Name = faculty.Name
                };
                return View(model);
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFacultyViewModel model)
        {
            int er = 0;
            Faculty faculty = await db.Faculties.FirstOrDefaultAsync(t => t.Id == model.Id);
            if (ModelState.IsValid && (model.Name == faculty.Name || (er = db.Faculties.Count(p => p.Name == model.Name)) == 0))
            {
                if (faculty == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! Прислана пустая модель"
                    };
                    return View("Error", error);
                }
                faculty.Name = model.Name;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("Name", "Факультет с таким именем уже есть");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Faculty faculty = await db.Faculties.FirstOrDefaultAsync(t => t.Id == id);
            db.Faculties.Remove(faculty);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Groups(int? id)
        {
            if (id != null)
            {
                Faculty faculty = await db.Faculties.Include("Groups").FirstOrDefaultAsync(t => t.Id == id);
                if (faculty == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! В базе данных отсутствует запись с переданным id = " + id
                    };
                    return View("Error", error);
                }
                return View(faculty);
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public async Task<IActionResult> CreateGroup(int? id)
        {
            if (id != null)
            {
                Faculty faculty = await db.Faculties.FirstOrDefaultAsync(t => t.Id == id);
                if (faculty != null)
                {
                    ViewBag.FacultyName = faculty.Name;
                    ViewBag.FacultyId = id;
                    return View();
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! В базе данных отсутствует " +
                        "запись факультета с переданным id = " + id
                    };
                    return View("Error", error);
                }
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(CreateGroupViewModel model)
        {
            int er = 0;
            if (ModelState.IsValid && (er = db.Groups.Count(p => p.Name == model.Name)) == 0)
            {
                Group group = new Group
                {
                    FacultyId = model.FacultyId,
                    Name = model.Name,
                    Course = model.Course
                };
                await db.Groups.AddAsync(group);
                await db.SaveChangesAsync();
                return RedirectToAction("Groups", new { id = model.FacultyId });
            }
            if (er != 0)
                ModelState.AddModelError("Name", "Группа с таким именем уже есть");
            return View(model);
        }

        public async Task<IActionResult> EditGroup(int? id)
        {
            if (id != null)
            {
                Group group = await db.Groups.Include("Faculty").FirstOrDefaultAsync(t => t.Id == id);
                if (group != null)
                {
                    EditGroupViewModel model = new EditGroupViewModel
                    {
                        Id = group.Id,
                        Name = group.Name,
                        Faculty = group.Faculty.Name,
                        Course = group.Course
                    };
                    return View(model);
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! В базе данных отсутствует " +
                        "запись группы с переданным id = " + id
                    };
                    return View("Error", error);
                }
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditGroup(EditGroupViewModel model)
        {
            Group group = await db.Groups.FirstOrDefaultAsync(t => t.Id == model.Id);
            int er = 0;
            if (ModelState.IsValid && (model.Name == group.Name || (er = db.Groups.Count(p => p.Name == model.Name)) == 0))
            {
                if (group == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! Прислана пустая модель"
                    };
                    return View("Error", error);
                }
                group.Name = model.Name;
                group.Course = model.Course;
                await db.SaveChangesAsync();
                return RedirectToAction("Groups", new { id = group.FacultyId });
            }
            if (er != 0)
                ModelState.AddModelError("Name", "Группа с таким именем уже есть");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroup(int? id)
        {
            Group group = await db.Groups.FirstOrDefaultAsync(t => t.Id == id);
            int GroupId = group.FacultyId;
            db.Groups.Remove(group);
            await db.SaveChangesAsync();
            return RedirectToAction("Groups", new { id = GroupId });
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            ErrorViewModel error = new ErrorViewModel
            {
                RequestId = "Не-не-не! Доступ только для преподавателей!"
            };
            return View("Error", error);
        }
    }
}