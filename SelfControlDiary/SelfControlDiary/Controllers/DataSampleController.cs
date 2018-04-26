using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SelfControlDiary.Controllers
{
    public class DataSampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}