using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class CreateStandardControlViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int StudentId { get; set; }
        [Range(400, 2000, ErrorMessage = "Дистанцию нужно указывать в метрах")]
        public int Run { get; set; }
        [Range(-20, 30, ErrorMessage = "Значение наклона должно попадать в промежуток от -20 до 30")]
        public int Incline { get; set; }
        [Range(0, 100, ErrorMessage = "Кол-во отжиманий должно быть положительным числом")]
        public int Bending { get; set; }
        public int? Pulling { get; set; }
        [Range(0, 150, ErrorMessage = "Кол-во приседаний должно быть положительным числом")]
        public int Squatting { get; set; }
        public int? Press { get; set; }

        [Range(1, 8, ErrorMessage = "Некорректный вариант для поля Семестр, введите значение в промежутке 1-8")]
        public int Semestr { get; set; }
    }
}
