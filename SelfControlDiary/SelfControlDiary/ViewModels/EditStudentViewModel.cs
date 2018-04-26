using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class EditStudentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле группа - обязательное для заполнения")]
        public int GroupId { get; set; }
        public int FacultyId { get; set; }
        [Required(ErrorMessage = "Поле Имя - обязательное для заполнения")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Поле Фамилия - обязательное для заполнения")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Поле пол - обязательное для заполнения")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Поле дата рождения - обязательное для заполнения")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
