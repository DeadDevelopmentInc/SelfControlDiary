using SelfControlDiary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class StandardControlListViewModel
    {
        public Student Stud { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Run { get; set; }
        public int Incline { get; set; }
        public int Bending { get; set; }
        public int? Pulling { get; set; }
        public int Squatting { get; set; }
        public int? Press { get; set; }
        public int Semestr { get; set; }

        public string GetRunVerdict()
        {
            return IndicatorRatesNorms.GetRunRes(Run, Stud.Sex, Semestr % 2 == 0 ?
                Semestr / 2 : Semestr / 2 + 1).ToString();
        }

        public string GetInclineVerdict()
        {
            return IndicatorRatesNorms.GetInclineRes(Incline, Stud.Sex, Semestr % 2 == 0 ?
                Semestr / 2 : Semestr / 2 + 1).ToString();
        }

        public string GetBendingVerdict()
        {
            return IndicatorRatesNorms.GetBendingRes(Bending, Stud.Sex, Semestr % 2 == 0 ?
                Semestr / 2 : Semestr / 2 + 1).ToString();
        }

        public string GetSquattingVerdict()
        {
            return IndicatorRatesNorms.GetSquattingRes(Squatting, Stud.Sex, Semestr % 2 == 0 ?
                Semestr / 2 : Semestr / 2 + 1).ToString();
        }

        public string GetPullingVerdict()
        {
            return IndicatorRatesNorms.GetPullingRes((int)Pulling, Semestr % 2 == 0 ?
                Semestr / 2 : Semestr / 2 + 1).ToString();
        }

        public string GetPressVerdict()
        {
            return IndicatorRatesNorms.GetPullingRes((int)Press, Semestr % 2 == 0 ?
                Semestr / 2 : Semestr / 2 + 1).ToString();
        }
    }
}
