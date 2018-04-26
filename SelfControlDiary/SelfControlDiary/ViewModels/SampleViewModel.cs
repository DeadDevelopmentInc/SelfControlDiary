using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class SampleViewModel
    {
        [Required(ErrorMessage = "Семестр 1 - обязательное поле")]
        [Range(1, 8, ErrorMessage = "Поле семестр 1 может иметь значение от 1-го до 8")]
        public int Sem1 { get; set; }
        [Required(ErrorMessage = "Семестр 2 - обязательное поле")]
        [Range(1, 8, ErrorMessage = "Поле семестр 2 может иметь значение от 1-го до 8")]
        public int Sem2 { get; set; }
        public int Fac1Id { get; set; }
        public int Fac2Id { get; set; }
        [Required(ErrorMessage = "Курс - обязательное поле")]
        [Range(1, 4, ErrorMessage = "Поле семестр 2 может иметь значение от 1-го до 8")]
        public int Course1 { get; set; }
        [Required(ErrorMessage = "Курс - обязательное поле")]
        [Range(1, 4, ErrorMessage = "Поле семестр 2 может иметь значение от 1-го до 8")]
        public int Course2 { get; set; }
        public string Sex { get; set; }
    }
}
