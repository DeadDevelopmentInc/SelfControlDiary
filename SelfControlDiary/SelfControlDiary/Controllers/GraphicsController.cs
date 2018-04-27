using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SelfControlDiary.Models;
using SelfControlDiary.ViewModels;

namespace SelfControlDiary.Controllers
{
    public class GraphicsController : Controller
    {
        private readonly DiaryContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GraphicsController(UserManager<User> userManager, SignInManager<User> signInManager, DiaryContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = context;
        }

        public IActionResult GetVRStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetVRIndexPoints((int)id, db);
                
                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Весоростовой индекс";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetLifeStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetLifeIndexPoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Жизненный индекс индекс";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetLeftWristStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetLeftWristPowerIndexPoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Силовой индекс левой кисти";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetRightWristStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetRightWristPowerIndexPoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Силовой индекс правой кисти";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetBaseForceStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetBaseForcePoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Становый индекс";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetAPSKStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetAPSKPoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "АПСК";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetKremptonStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetBaseForcePoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Показатель Кремптона";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetKerdoStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetKerdoPoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Вегетативный индекс Кердо";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetStaminaCoefStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetStaminaCoefPoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Коэффициент выносливости";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetUFSStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetUFSPoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "УФС";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetRobinsonStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetRobinsonIndexPoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Индекс Робинсона";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetWeightStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetWeightIndexPoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Индекс массы тела";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetGenchiStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetGenchePoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Проба Генчи";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetShtangeStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetShtangePoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Проба Штанге";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetCCCStatistic(int? id, int? listId)
        {
            if (id != null)
            {
                var tmp = StudentsPoints.GetCCCPoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Tit = "Функциональная проба ССС";
                ViewBag.Id = listId;
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

        public IActionResult GetStatStatistic(int? id, int? listId)
        {
            if (id != null && listId != null)
            {
                var tmp = StudentsPoints.GetStatPoints((int)id, db);

                List<DataPoint> list = new List<DataPoint>();
                for (int i = 0; i < tmp.Item2.Length; i++)
                {
                    list.Add(new DataPoint(tmp.Item1[i], tmp.Item2[i]));
                }
                ViewBag.DataPointsUser = JsonConvert.SerializeObject(list);
                ViewBag.Id = listId;
                ViewBag.Tit = "Ортостатическая проба";
                return View("Index");
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Ошибка! Отсутствует id в параметрах запроса" };
                return View("Error", error);
            }
        }

    }
}