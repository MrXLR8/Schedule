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
                case "ScheduleUpload":
                    ScheduleUpload();
                    return;
                case "ScheduleDownload":
                    ScheduleDownload();
                    return;
                default:
                    toAnswer = "bad";
                    return;
            }
        }

        public void ScheduleHashVerify()
        {
            Console.WriteLine("[CMD]Клиент запросил сравнение актуальности расписаний");

            if (Global.MainSchedule == null || Global.MainSchedule.groupList.Count == 0)
            { toAnswer = "different"; Console.WriteLine("[CMD]Расписания отличаются"); return; }


            string myHash = Global.MainSchedule.hash();
            if (myHash == arguments[0]) { toAnswer = "same"; Console.WriteLine("[CMD]Расписания идентичны"); }
            else { toAnswer = "different"; Console.WriteLine("[CMD]Расписания отличаются"); }


            
        }

      public void ScheduleUpload()
        {
            Console.WriteLine("[CMD]Клиент пытаеться внести свое расписание");
            Schedule recived;
            try
            {
                string decrypted = Cipher.transcript(arguments[0]);
                recived = JsonConvert.DeserializeObject<Schedule>(decrypted);
                if (recived == null || recived.groupList.Count == 0) throw new Exception();
                if(recived.hash()!=arguments[1]) throw new Exception();
            }
            catch (Exception e) {
                Console.WriteLine("[CMD]Расписание полученно некорректно, или оно не содержит групп");
                toAnswer = "bad";
                return;
            }

            Global.MainSchedule = recived;
            Console.WriteLine("[CMD]Установленно новое расписание. Количество групп: "+recived.groupList.Count);
            Console.WriteLine("[CMD]Создано на ПК :"+recived.pcName);
            toAnswer = "accepted";

        }

        public void ScheduleDownload()
        {
            Console.WriteLine("[CMD]Клиент запрашивает загрузку расписания");
            try
            {
                string json = JsonConvert.SerializeObject(Global.MainSchedule);
                string hash = Global.MainSchedule.hash();
                string encrypt = Cipher.encryption(json);

                Command answer = new Command();
                answer.type = "ScheduleDownload";
                answer.arguments.Add(encrypt);
                answer.arguments.Add(hash);
                toAnswer = JsonConvert.SerializeObject(answer);
            }
            catch (Exception e)
            {
                Console.WriteLine("[CMD]Расписание не удалось отправить");
                toAnswer = "bad";
                return;
            }

            Console.WriteLine("[CMD]Расписание отправлено");


        }

    }
}

