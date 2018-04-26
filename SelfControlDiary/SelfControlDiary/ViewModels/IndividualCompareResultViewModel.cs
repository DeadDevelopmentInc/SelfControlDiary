using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class IndividualCompareResultViewModel
    {
        public IndicatorsListViewModel l1 { get; set; }
        public IndicatorsListViewModel l2 { get; set; }
        public string Sex { get; set; }
    }
}
