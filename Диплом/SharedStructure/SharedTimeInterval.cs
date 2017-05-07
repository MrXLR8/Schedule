using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{

    public partial class IntervalCollection 
    {
    
        public IntervalCollection() { }

        public int last { get { return timeList.Count; } }

        public ObservableCollection<Interval> timeList = new ObservableCollection<Interval>();

    }

    public partial class Interval 
    {
        public int index;
        public TimeSpan start;
        public TimeSpan end;

        public Interval() { }

    }
}
    
