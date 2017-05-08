using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
   public static class Global
    {
        public static Schedule MainSchedule;


        public static Group getGroup(Schedule input,string groupName)
        {
            foreach(Group g in MainSchedule.groupList)
            {
                if (g.name == groupName) return g;
            }
            return null;
        }
    }
}
