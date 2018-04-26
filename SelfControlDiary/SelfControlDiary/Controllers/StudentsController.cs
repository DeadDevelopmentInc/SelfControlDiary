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
    public class StudentsController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DiaryContext db;

        public StudentsController(UserManager<User> userManager, SignInManager<User> signInManager, DiaryContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = context;
        }

        public async Task<IActionResult> Index(string lastName, int? group, int? faculty, int? course, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            int pageSize = 10;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                List<IndexStudentsViewModel> list = new List<IndexStudentsViewModel>();
                foreach (var item in db.Students.Include("Group"))
                {
                    list.Add(new IndexStudentsViewModel
                    {
                        Id = item.Id,
                        GroupId = item.GroupId,
                        GroupName = item.Group.Name,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Sex = item.Sex,
                        BirthDate = item.BirthDate,
                        FacultyId = item.Group.FacultyId,
                        Course = item.Group.Course
                    });
                }
                IQueryable<IndexStudentsViewModel> filterList = list.AsQueryable();
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
                            filterList = filterList.OrderBy(s => s.GroupName);
                            break;
                        }
                    case SortState.GroupDesc:
                        {
                            filterList = filterList.OrderByDescending(s => s.GroupName);
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
                StudentListViewModel model = new StudentListViewModel
                {
                    PageViewModel = new PageViewModel(count, page, pageSize),
                    SortViewModel = new SortViewModel(sortOrder),
                    FilterViewModel = new FilterViewModel(lastName, db.Groups.ToList(), group, db.Faculties.ToList(), faculty, course),
                    Students = items
                };
                return View(model);
            }
            else return RedirectToAction("Index", "LK");
        }



        //public IActionResult Create()
        //{
        //    Dictionary<string, int> list = new Dictionary<string, int>();
        //    foreach (var item in db.Groups)
        //    {
        //        list.Add(item.Name, item.Id);
        //    }
        //    CreateStudentViewModel model = new CreateStudentViewModel
        //    {
        //        Groups = list
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(CreateStudentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Student student = new Student
        //        {
        //            GroupId = model.GroupId,
        //            FirstName = model.FirstName,
        //            LastName = model.LastName,
        //            Sex = model.Sex,
        //            BirthDate = model.BirthDate
        //        };
        //        await db.Students.AddAsync(student);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}

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

        public IActionResult GetGroups(int id)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach (var item in db.Groups.Where(p => p.FacultyId == id))
            {
                list.Add(item.Name, item.Id);
            }
            RegisterChangeFacultyViewModel model = new RegisterChangeFacultyViewModel
            {
                Groups = list
            };
            return PartialView(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                if (await CheckAccess((int)id))
                {
                    Student student = await db.Students.Include("Group").FirstOrDefaultAsync(t => t.Id == id);
                    if (student == null)
                    {
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "Ошибка! В базе данных отсутствует запись с переданным id = " + id
                        };
                        return View("Error", error);
                    }
                    Dictionary<string, int> list2 = new Dictionary<string, int>();
                    foreach (var item in db.Faculties)
                    {
                        list2.Add(item.Name, item.Id);
                    }
                    Dictionary<string, int> list1 = new Dictionary<string, int>();
                    foreach (var item in db.Groups.Where(p => p.FacultyId == student.Group.FacultyId))
                    {
                        list1.Add(item.Name, item.Id);
                    }
                    EditStudentViewModel model = new EditStudentViewModel
                    {
                        Id = student.Id,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        BirthDate = student.BirthDate,
                        Sex = student.Sex,
                        GroupId = student.GroupId,
                        FacultyId = student.Group.FacultyId
                    };
                    ViewBag.Groups = list1;
                    ViewBag.Faculties = list2;
                    return View(model);
                }
                else return RedirectToAction("AccessDenied");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditStudentViewModel model)
        {
            Student student = await db.Students.Include("Group").FirstOrDefaultAsync(t => t.Id == model.Id);
            if (ModelState.IsValid && model.GroupId != 0)
            {
                if (await CheckAccess(model.Id))
                {
                    if (student == null)
                    {
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "Ошибка! Прислана пустая модель"
                        };
                        return View("Error", error);
                    }
                    student.GroupId = model.GroupId;
                    student.FirstName = model.FirstName;
                    student.LastName = model.LastName;
                    student.Sex = model.Sex;
                    student.BirthDate = model.BirthDate;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else return RedirectToAction("AccessDenied");
            }
            Dictionary<string, int> list2 = new Dictionary<string, int>();
            foreach (var item in db.Faculties)
            {
                list2.Add(item.Name, item.Id);
            }
            Dictionary<string, int> list1 = new Dictionary<string, int>();
            foreach (var item in db.Groups.Where(p => p.FacultyId == student.Group.FacultyId))
            {
                list2.Add(item.Name, item.Id);
            }
            ViewBag.Groups = list1;
            ViewBag.Faculties = list2;
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Student student = await db.Students.FirstOrDefaultAsync(t => t.Id == id);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> IndicatorLists(int? id)
        {
            if (id != null)
            {
                if (await CheckAccess((int)id))
                {
                    Student student = await db.Students.Include("IndicatorLists").FirstOrDefaultAsync(t => t.Id == id);
                    if (student == null)
                    {
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "Ошибка! В базе данных отсутствует запись студента с переданным id = " + id
                        };
                        return View("Error", error);
                    }
                    return View(student);
                }
                else return RedirectToAction("AccessDenied");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }


        public async Task<IActionResult> CreateList(int? id)
        {
            if (id != null)
            {
                if (await CheckAccess((int)id))
                {
                    Student student = await db.Students.FirstOrDefaultAsync(t => t.Id == id);
                    if (student != null)
                    {
                        ViewBag.Student = student.FirstName + " " + student.LastName;
                        ViewBag.StudentId = id;
                        ViewBag.Sex = student.Sex;
                        return View();
                    }
                    else
                    {
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "Ошибка! В базе данных отсутствует " +
                            "запись студента с переданным id = " + id
                        };
                        return View("Error", error);
                    }
                }
                else return RedirectToAction("AccessDenied");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateList(CreateIndicatorListViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await CheckAccess(model.StudentId))
                {
                    var student = await db.Students.Include("Group").FirstOrDefaultAsync(p => p.Id == model.StudentId);
                    if (db.IndicatorLists.Where(p => p.Semestr == model.Semestr && p.StudentId == model.StudentId).Count() == 0)
                    {
                        IndicatorList list = new IndicatorList
                        {
                            StudentId = model.StudentId,
                            Date = model.Date,
                            Semestr = model.Semestr,
                            Height = model.Height,
                            Weight = model.Weight,
                            JEL = model.JEL,
                            BaseForce = model.BaseForce,
                            LeftWrist = model.LeftWrist,
                            RightWrist = model.RightWrist,
                            Pause = model.Pause,
                            Breath = model.Breath,
                            Exhalation = model.Exhalation,
                            ChSS = model.ChSS,
                            ADD = model.ADD,
                            ADS = model.ADS,
                            Run = model.Run,
                            Bending = model.Bending,
                            CCC = model.CCC,
                            Genchi = model.Genchi,
                            Incline = model.Incline,
                            Press = model.Press,
                            Pulling = model.Pulling,
                            Shtange = model.Shtange,
                            Squatting = model.Squatting,
                            Stat = model.Stat
                        };
                        await db.IndicatorLists.AddAsync(list);
                        await db.SaveChangesAsync();
                        return RedirectToAction("IndicatorLists", new { id = model.StudentId });
                    }
                    else return View("Error", new ErrorViewModel { RequestId = "В указанном семестре уже был произведён замер показателей, исправьте уже созданный" });
                }
                else return RedirectToAction("AccessDenied");
            }
            Student stud = await db.Students.FirstOrDefaultAsync(t => t.Id == model.StudentId);
            ViewBag.Sex = stud.Sex;
            ViewBag.StudentId = model.StudentId;
            ViewBag.Student = stud.FirstName + " " + stud.LastName;
            return View(model);
        }

        public async Task<IActionResult> IndividualCompare(int? id)
        {
            if (id != null)
            {
                if (await CheckAccess((int)id))
                {
                    Student student = await db.Students.FirstOrDefaultAsync(t => t.Id == id);
                    if (student != null)
                    {
                        ViewBag.Student = student.FirstName + " " + student.LastName;
                        ViewBag.StudentId = id;
                        return View();
                    }
                    else
                    {
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "Ошибка! В базе данных отсутствует " +
                            "запись студента с переданным id = " + id
                        };
                        return View("Error", error);
                    }
                }
                else return RedirectToAction("AccessDenied");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> IndividualCompare(IndividualCompareViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    Student student = await db.Students.FirstOrDefaultAsync(p => p.Id == model.StudentId);
                    if (student == null)
                    {
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "Ошибка! В базе данных отсутствует пользователь с id = " + model.StudentId
                        };
                        return View("Error", error);
                    }
                    if (await CheckAccess(model.StudentId))
                    {
                        IndicatorList list1 = await db.IndicatorLists.Include("Student").FirstOrDefaultAsync(t => t.StudentId == model.StudentId && t.Semestr == model.Sem1);
                        IndicatorList list2 = await db.IndicatorLists.Include("Student").FirstOrDefaultAsync(t => t.StudentId == model.StudentId && t.Semestr == model.Sem2);
                        if (list1 != null && list2 != null)
                        {
                            IndicatorsListViewModel l1 = new IndicatorsListViewModel
                            {
                                Stud = list1.Student,
                                Age = list1.Student.BirthDate.DayOfYear <= DateTime.Now.DayOfYear ? DateTime.Now.Year - list1.Student.BirthDate.Year :
                                DateTime.Now.Year - list1.Student.BirthDate.Year - 1,
                                Date = list1.Date,
                                Semestr = list1.Semestr,
                                Height = list1.Height,
                                Weight = list1.Weight,
                                JEL = list1.JEL,
                                BaseForce = list1.BaseForce,
                                LeftWrist = list1.LeftWrist,
                                RightWrist = list1.RightWrist,
                                Pause = list1.Pause,
                                Breath = list1.Breath,
                                Exhalation = list1.Exhalation,
                                ChSS = list1.ChSS,
                                ADD = list1.ADD,
                                ADS = list1.ADS,
                                Run = list1.Run,
                                Bending = list1.Bending,
                                CCC = list1.CCC,
                                Genchi = list1.Genchi,
                                Incline = list1.Incline,
                                Press = list1.Press,
                                Pulling = list1.Pulling,
                                Shtange = list1.Shtange,
                                Squatting = list1.Squatting,
                                Stat = list1.Stat
                            };
                            IndicatorsListViewModel l2 = new IndicatorsListViewModel
                            {
                                Stud = list2.Student,
                                Age = list2.Student.BirthDate.DayOfYear <= DateTime.Now.DayOfYear ? DateTime.Now.Year - list2.Student.BirthDate.Year :
                                DateTime.Now.Year - list2.Student.BirthDate.Year - 1,
                                Date = list2.Date,
                                Semestr = list2.Semestr,
                                Height = list2.Height,
                                Weight = list2.Weight,
                                JEL = list2.JEL,
                                BaseForce = list2.BaseForce,
                                LeftWrist = list2.LeftWrist,
                                RightWrist = list2.RightWrist,
                                Pause = list2.Pause,
                                Breath = list2.Breath,
                                Exhalation = list2.Exhalation,
                                ChSS = list2.ChSS,
                                ADD = list2.ADD,
                                ADS = list2.ADS,
                                Run = list2.Run,
                                Bending = list2.Bending,
                                CCC = list2.CCC,
                                Genchi = list2.Genchi,
                                Incline = list2.Incline,
                                Press = list2.Press,
                                Pulling = list2.Pulling,
                                Shtange = list2.Shtange,
                                Squatting = list2.Squatting,
                                Stat = list2.Stat
                            };
                            IndividualCompareResultViewModel mod = new IndividualCompareResultViewModel
                            {
                                l1 = l1,
                                l2 = l2,
                                Sex = student.Sex
                            };
                            return View("CompareResult", mod);
                        }
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "У студента " + student.LastName + " " +
                            student.FirstName + " нет замера показателей как минимум за один из указанных семестров"
                        };
                        return View("Error", error);
                    }
                    else return RedirectToAction("AccessDenied");
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Прислана пустая модель" };
                    return View("Error", error);
                }
            }
            Student stud = await db.Students.FirstOrDefaultAsync(t => t.Id == model.StudentId);
            ViewBag.Student = stud.LastName + " " + stud.FirstName;
            ViewBag.StudentId = stud.Id;
            return View(model);
        }

        public async Task<IActionResult> EditList(int? id)
        {
            if (id != null)
            {
                IndicatorList list = await db.IndicatorLists.Include("Student").FirstOrDefaultAsync(t => t.Id == id);
                if (list != null && await CheckAccess(list.StudentId))
                {
                    if (list != null)
                    {
                        EditIndicatorListViewModel model = new EditIndicatorListViewModel
                        {
                            Id = list.Id,
                            Student = list.Student.FirstName + " " + list.Student.LastName,
                            Date = list.Date,
                            Semestr = list.Semestr,
                            Height = list.Height,
                            Weight = list.Weight,
                            JEL = list.JEL,
                            BaseForce = list.BaseForce,
                            LeftWrist = list.LeftWrist,
                            RightWrist = list.RightWrist,
                            Pause = list.Pause,
                            Breath = list.Breath,
                            Exhalation = list.Exhalation,
                            ChSS = list.ChSS,
                            ADD = list.ADD,
                            ADS = list.ADS,
                            Run = list.Run,
                            Bending = list.Bending,
                            CCC = list.CCC,
                            Genchi = list.Genchi,
                            Incline = list.Incline,
                            Press = list.Press,
                            Pulling = list.Pulling,
                            Shtange = list.Shtange,
                            Squatting = list.Squatting,
                            Stat = list.Stat,
                            Sex = list.Student.Sex
                        };
                        return View(model);
                    }
                    else
                    {
                        ErrorViewModel error = new ErrorViewModel
                        {
                            RequestId = "Ошибка! В базе данных отсутствует " +
                            "запись с переданным id = " + id
                        };
                        return View("Error", error);
                    }
                }
                else return RedirectToAction("AccessDenied");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditList(EditIndicatorListViewModel model)
        {
            if (ModelState.IsValid)
            {
                IndicatorList list = await db.IndicatorLists.FirstOrDefaultAsync(t => t.Id == model.Id);
                if (list == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! Прислана пустая модель"
                    };
                    return View("Error", error);
                }
                if (await CheckAccess(list.StudentId))
                {
                    var student = await db.Students.Include("Group").FirstOrDefaultAsync(p => p.Id == list.StudentId);
                    var l = await db.IndicatorLists.Where(p => p.Semestr == model.Semestr && p.StudentId == list.StudentId).ToListAsync();
                    if (l.Count() == 0 || model.Semestr == list.Semestr)
                    {
                        list.Date = model.Date;
                        list.Semestr = model.Semestr;
                        list.Height = model.Height;
                        list.Weight = model.Weight;
                        list.JEL = model.JEL;
                        list.BaseForce = model.BaseForce;
                        list.LeftWrist = model.LeftWrist;
                        list.RightWrist = model.RightWrist;
                        list.Pause = model.Pause;
                        list.Breath = model.Breath;
                        list.Exhalation = model.Exhalation;
                        list.ChSS = model.ChSS;
                        list.ADD = model.ADD;
                        list.ADS = model.ADS;
                        list.Run = model.Run;
                        list.Bending = model.Bending;
                        list.CCC = model.CCC;
                        list.Genchi = model.Genchi;
                        list.Incline = model.Incline;
                        list.Press = model.Press;
                        list.Pulling = model.Pulling;
                        list.Shtange = model.Shtange;
                        list.Squatting = model.Squatting;
                        list.Stat = model.Stat;
                        await db.SaveChangesAsync();
                        return RedirectToAction("IndicatorLists", new { id = list.StudentId });
                    }
                    return View("Error", new ErrorViewModel { RequestId = "В указанном семестре уже был произведён замер показателей, исправьте уже созданный" });
                }
                else return RedirectToAction("AccessDenied");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteList(int? id)
        {
            IndicatorList list = await db.IndicatorLists.FirstOrDefaultAsync(t => t.Id == id);
            if (list == null)
            {
                ErrorViewModel error = new ErrorViewModel
                {
                    RequestId = "Ошибка! В базе данных отсутствует запись замера с переданным id = " + id
                };
                return View("Error", error);
            }
            if (await CheckAccess(list.StudentId))
            {
                int StudentId = list.StudentId;
                db.IndicatorLists.Remove(list);
                await db.SaveChangesAsync();
                return RedirectToAction("IndicatorLists", new { id = StudentId });
            }
            else return RedirectToAction("AccessDenied");
        }

        public async Task<IActionResult> ShowList(int? id)
        {
            if (id != null)
            {
                IndicatorList list = await db.IndicatorLists.Include("Student").FirstOrDefaultAsync(t => t.Id == id);
                if (list == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Ошибка! В базе данных отсутствует запись замера с переданным id = " + id
                    };
                    return View("Error", error);
                }
                if (await CheckAccess(list.StudentId))
                {
                    IndicatorsListViewModel model = new IndicatorsListViewModel
                    {
                        Stud = list.Student,
                        Age = list.Student.BirthDate.DayOfYear <= DateTime.Now.DayOfYear ? DateTime.Now.Year - list.Student.BirthDate.Year :
                        DateTime.Now.Year - list.Student.BirthDate.Year - 1,
                        Date = list.Date,
                        Semestr = list.Semestr,
                        Height = list.Height,
                        Weight = list.Weight,
                        JEL = list.JEL,
                        BaseForce = list.BaseForce,
                        LeftWrist = list.LeftWrist,
                        RightWrist = list.RightWrist,
                        Pause = list.Pause,
                        Breath = list.Breath,
                        Exhalation = list.Exhalation,
                        ChSS = list.ChSS,
                        ADD = list.ADD,
                        ADS = list.ADS,
                        Run = list.Run,
                        Bending = list.Bending,
                        CCC = list.CCC,
                        Genchi = list.Genchi,
                        Incline = list.Incline,
                        Press = list.Press,
                        Pulling = list.Pulling,
                        Shtange = list.Shtange,
                        Squatting = list.Squatting,
                        Stat = list.Stat
                    };
                    return View(model);
                }
                else return RedirectToAction("AccessDenied");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        async Task<bool> CheckAccess(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (!(await _userManager.IsInRoleAsync(user, "admin") || user.StudentId == id))
            {
                return false;
            }
            return true;
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            ErrorViewModel error = new ErrorViewModel
            {
                RequestId = "Не-не-не! Это не твоё!"
            };
            return View("Error", error);
        }


    }
}