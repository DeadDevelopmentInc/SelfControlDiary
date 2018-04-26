using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class CreateIndicatorListViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int StudentId { get; set; }
        [Range(1, 8, ErrorMessage = "Некорректный вариант для поля Семестр, введите значение в промежутке 1-8")]
        public int Semestr { get; set; }
        [Range(100, 230, ErrorMessage = "Рост нужно указывать в сантиметрах")]
        public int Height { get; set; }
        [Range(30, 150, ErrorMessage = "Вес нужно указывать в киллограммах")]
        public int Weight { get; set; }
        [Range(1500, 6000, ErrorMessage = "Жизненную ёмкость лёгких нужно указывать в мл")]
        public int JEL { get; set; }
        [Range(30, 250, ErrorMessage = "Становую силу нужно указывать в килограммах")]
        public int BaseForce { get; set; }
        [Range(10, 150, ErrorMessage = "Силу кисти нужно указывать в килограммах")]
        public int LeftWrist { get; set; }
        [Range(10, 150, ErrorMessage = "Силу кисти нужно указывать в килограммах")]
        public int RightWrist { get; set; }
        [Range(50, 150, ErrorMessage = "Пауза в сантиметрах от 50 до 150")]
        public int Pause { get; set; }
        [Range(50, 150, ErrorMessage = "Вдох в сантиметрах от 50 до 150")]
        public int Breath { get; set; }
        [Range(50, 150, ErrorMessage = "Выдох в сантиметрах от 50 до 150")]
        public int Exhalation { get; set; }
        [Range(50, 200, ErrorMessage = "ЧСС нужно указывать в ударах в минуту")]
        public int ChSS { get; set; }
        [Range(80, 250, ErrorMessage = "АДС нужно указывать в ударах в минуту")]
        public int ADS { get; set; }
        [Range(50, 250, ErrorMessage = "АДД нужно указывать в ударах в минуту")]
        public int ADD { get; set; }
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
        [Range(1, 100, ErrorMessage = "Проба Генчи указывается в секундах")]
        public int Genchi { get; set; }
        [Range(1, 180, ErrorMessage = "Проба Штанге указывается в секундах")]
        public int Shtange { get; set; }
        [Range(1, 240, ErrorMessage = "Функциональная проба ССС указывается в секундах")]
        public int CCC { get; set; }
        [Range(-15, 50, ErrorMessage = "Ортостатическая проба может иметь значение в промежутке от -15 до 50")]
        public int Stat { get; set; }
    }
}
