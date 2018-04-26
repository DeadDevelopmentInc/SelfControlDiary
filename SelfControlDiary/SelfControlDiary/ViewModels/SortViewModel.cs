using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfControlDiary.ViewModels
{
    public class SortViewModel
    {
        public SortState NameSort { get; private set; }
        public SortState GroupSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            GroupSort = sortOrder == SortState.GroupAsc ? SortState.GroupDesc : SortState.GroupAsc;
            Current = sortOrder;
        }
    }
}
