using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SelfControlDiary.Controllers
{
    public class FacultiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}