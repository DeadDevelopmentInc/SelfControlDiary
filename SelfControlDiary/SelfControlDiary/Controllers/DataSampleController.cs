using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfControlDiary.Models;
using SelfControlDiary.ViewModels;

namespace SelfControlDiary.Controllers
{
    [Authorize(Roles = "admin")]
    public class DataSampleController : Controller
    {
        DiaryContext db;

        public DataSampleController(DiaryContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            list.Add("Все", -1);
            foreach (var item in db.Faculties)
            {
                list.Add(item.Name, item.Id);
            }
            ViewBag.Faculties = list;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SampleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var list1 = new List<IndicatorsListViewModel>();
                var list2 = new List<IndicatorsListViewModel>();
                var v = await db.IndicatorLists.Include(x => x.Student).Include("Student.Group.Faculty").ToListAsync();
                var v1 = v.Where(p => p.Semestr == model.Sem1 && model.Sex == p.Student.Sex && p.Student.Group.Course == model.Course1);
                var v2 = v.Where(p => p.Semestr == model.Sem2 && model.Sex == p.Student.Sex && p.Student.Group.Course == model.Course2);
                if (model.Fac1Id != -1)
                {
                    v1 = v1.Where(p => p.Student.Group.FacultyId == model.Fac1Id);
                }
                if (model.Fac2Id != -1)
                {
                    v2 = v2.Where(p => p.Student.Group.FacultyId == model.Fac2Id);
                }
                foreach (var item in v1)
                {
                    list1.Add(new IndicatorsListViewModel()
                    {
                        Stud = item.Student,
                        Age = item.Student.BirthDate.DayOfYear <= DateTime.Now.DayOfYear ? DateTime.Now.Year - item.Student.BirthDate.Year :
                        DateTime.Now.Year - item.Student.BirthDate.Year - 1,
                        Date = item.Date,
                        Semestr = item.Semestr,
                        Height = item.Height,
                        Weight = item.Weight,
                        JEL = item.JEL,
                        BaseForce = item.BaseForce,
                        LeftWrist = item.LeftWrist,
                        RightWrist = item.RightWrist,
                        Pause = item.Pause,
                        Breath = item.Breath,
                        Exhalation = item.Exhalation,
                        ChSS = item.ChSS,
                        ADD = item.ADD,
                        ADS = item.ADS,
                        CCC = item.CCC,
                        Genchi = item.Genchi,
                        Stat = item.Stat
                    });
                }
                foreach (var item in v2)
                {
                    list2.Add(new IndicatorsListViewModel()
                    {
                        Stud = item.Student,
                        Age = item.Student.BirthDate.DayOfYear <= DateTime.Now.DayOfYear ? DateTime.Now.Year - item.Student.BirthDate.Year :
                        DateTime.Now.Year - item.Student.BirthDate.Year - 1,
                        Date = item.Date,
                        Semestr = item.Semestr,
                        Height = item.Height,
                        Weight = item.Weight,
                        JEL = item.JEL,
                        BaseForce = item.BaseForce,
                        LeftWrist = item.LeftWrist,
                        RightWrist = item.RightWrist,
                        Pause = item.Pause,
                        Breath = item.Breath,
                        Exhalation = item.Exhalation,
                        ChSS = item.ChSS,
                        ADD = item.ADD,
                        ADS = item.ADS,
                        CCC = item.CCC,
                        Genchi = item.Genchi,
                        Shtange = item.Shtange,
                        Stat = item.Stat
                    });
                }
                if (list1.Count == 0 || list2.Count == 0)
                {
                    ErrorViewModel error = new ErrorViewModel { RequestId = "О студентах выбранной группы нет данных за какой-либо из выбранных семестров" };
                    return View("Error", error);
                }
                ViewBag.Fac1 = v1.ElementAt(0).Student.Group.Faculty.Name;
                ViewBag.Fac2 = v2.ElementAt(0).Student.Group.Faculty.Name;
                SampleListViewModel sample = new SampleListViewModel()
                {
                    List1 = list1,
                    List2 = list2,
                    Group = db.Students.Include("Group").FirstOrDefault(p => p.Id == list1[0].Stud.Id).Group.Name,
                    Sem1 = model.Sem1,
                    Sem2 = model.Sem2,
                    Sex = model.Sex,
                    Count1 = list1.Count,
                    Count2 = list2.Count
                };
                return View("SampleResult", sample);
            }
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach (var item in db.Faculties)
            {
                list.Add(item.Name, item.Id);
            }
            ViewBag.Faculties = list;
            return View(model);
        }

        public IActionResult AddGroupElem(int count)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach (var item in db.Groups)
            {
                list.Add(item.Name, item.Id);
            }
            DataSampleAddGroupViewModel model = new DataSampleAddGroupViewModel
            {
                Groups = list,
                Count = count
            };
            return PartialView(model);
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