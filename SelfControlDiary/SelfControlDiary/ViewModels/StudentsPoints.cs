using Microsoft.EntityFrameworkCore;
using SelfControlDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public static class StudentsPoints
    {
        public static IndicatorsListViewModel[] GetLists(int id, DiaryContext db)
        {
            var list = db.IndicatorLists.Include("Student").Where(t => t.StudentId == id);
            IndicatorsListViewModel[] res = new IndicatorsListViewModel[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = new IndicatorsListViewModel
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
                };
                i++;
            }
            return res;
        }

        public static (double[], double[]) GetVRIndexPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach(var item in list)
            {
                res[i] = item.GetVRIndex();
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetLifeIndexPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.GetLifeIndex();
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetLeftWristPowerIndexPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.GetLeftWristPowerIndex();
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetRightWristPowerIndexPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.GetRightWristPowerIndex();
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetBaseForcePoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.GetBaseForceIndex();
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetAPSKPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.GetAPSKIndex();
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetKerdoPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.GetKerdoIndex();
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetStaminaCoefPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.GetStaminaCoef();
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetUFSPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.GetUFS();
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetRobinsonIndexPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.GetRobinsonIndex();
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetWeightIndexPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.GetWeightIndex();
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetGenchePoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.Genchi;
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetShtangePoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.Shtange;
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetCCCPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.CCC;
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

        public static (double[], double[]) GetStatPoints(int id, DiaryContext db)
        {
            var list = GetLists(id, db);
            double[] res = new double[list.Count()];
            double[] dates = new double[list.Count()];
            int i = 0;
            foreach (var item in list)
            {
                res[i] = item.Stat;
                dates[i] = item.Date.Ticks;
                i++;
            }
            (double[], double[]) tup = (dates, res);
            return tup;
        }

    }
}
