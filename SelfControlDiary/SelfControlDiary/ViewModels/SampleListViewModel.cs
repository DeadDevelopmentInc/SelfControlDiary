using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class SampleListViewModel
    {
        public int Sem1 { get; set; }
        public int Sem2 { get; set; }
        public string Sex { get; set; }
        public string Group { get; set; }
        public List<IndicatorsListViewModel> List1 { get; set; }
        public List<IndicatorsListViewModel> List2 { get; set; }
        public int Count1 { get; set; }
        public int Count2 { get; set; }
    }
}
