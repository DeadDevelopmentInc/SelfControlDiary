using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class StudentChangePasswordViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Поле новый пароль - обязательное")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Поле старый пароль - обязательное")]
        public string OldPassword { get; set; }
    }
}
