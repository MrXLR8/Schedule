using System;
using System.Collections.Generic;
using System.Text;
using Builder;
using Newtonsoft.Json;
namespace Share
{
    public partial class Command
    {
        public void qualify()
        {
            switch (type)
            {
                case "ScheduleHashVerify":
                    ScheduleHashVerify();
                    return;
            }
        }

        public void ScheduleHashVerify()
        {
            Console.WriteLine("[CMD]Клиент запросил сравнение актуальности расписаний");
            if (Global.MainSchedule == null || Global.MainSchedule.groupList.Count == 0)
            { toAnswer = "different"; Console.WriteLine("[CMD]Расписания отличаются"); return; }
            string myHash=Data.Hash.GetMd5Hash(JsonConvert.SerializeObject(Global.MainSchedule));
            if (myHash == arguments[0]) { toAnswer = "same"; Console.WriteLine("[CMD]Расписания идентичны"); }
            else { toAnswer = "different"; Console.WriteLine("[CMD]Расписания отличаются"); }


            
        }

    }
}

