using Microsoft.AspNetCore.Mvc.Rendering;
using SelfControlDiary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(string lastName, List<Group> groups, int?group, List<Faculty> faculties, int? faculty, int? course)
        {
            SelectedStudent = lastName;
            groups.Insert(0, new Group { Name = "Все", Id = -1 });
            Groups = new SelectList(groups, "Id", "Name", group);
            faculties.Insert(0, new Faculty { Name = "Все", Id = -1 });
            Faculties = new SelectList(faculties, "Id", "Name", faculty);
            SelectedFaculty = faculty;
            SelectedGroup = group;
            SelectedCourse = course;
        }

        public string SelectedStudent { get; private set; }
        public SelectList Faculties { get; private set; }
        public SelectList Groups { get; private set; }
        [Range(1, 4, ErrorMessage = "Поле курс может иметь значение от 1 до 4")]
        public int? SelectedCourse { get; private set; }
        public int? SelectedFaculty { get; private set; }
        public int? SelectedGroup { get; private set; }
    }
}
