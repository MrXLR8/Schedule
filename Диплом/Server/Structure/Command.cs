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

        
        bool checkEmpty()
        {
            if (Global.MainSchedule == null || Global.MainSchedule.groupList.Count == 0)
            { toAnswer = "empty"; Console.WriteLine("[CMD] На сервере нет рабочего расписания!"); return true; }
            return false;
        }

        #region schedule

                public void ScheduleHashVerify()
                {
                    Console.WriteLine("[CMD]Клиент запросил сравнение актуальности расписаний");

                    if (checkEmpty()) { toAnswer = "different"; return; }


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

                if (checkEmpty()) { return; }

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

        #endregion


        #region group

        public void GroupHashVerify()
        {
            // группа 0. хеш 1
            Console.WriteLine("[CMD]Клиент запросил сравнение актуальности группы: "+arguments[0]);

            if(checkEmpty()) { return; }


            Group inside = Global.getGroup(Global.MainSchedule,arguments[0]);

            if(inside==null) { toAnswer = "noGroup"; return; }


            string myHash = inside.hash();
            if (myHash == arguments[1]) { toAnswer = "same"; Console.WriteLine("[CMD]Группы идентичны"); }
            else { toAnswer = "different"; Console.WriteLine("[CMD]Группы отличаются"); }


        }

        public void GroupListDownload()
        {
            Console.WriteLine("[CMD]Клиент запросил список всех групп" );

            if (checkEmpty()) { return; }

            List<string> groups = new List<string>();

            foreach(Group g in Global.MainSchedule.groupList)
            {
                groups.Add(g.name);
            }

            string json = JsonConvert.SerializeObject(groups);

            toAnswer = json;
        }

        public void GroupDownload()
        {
            Console.WriteLine("[CMD]Клиент запросил загрузку группы: " + arguments[0]);

            if (checkEmpty()) { return; }
            Group wanted = Global.getGroup(Global.MainSchedule, arguments[0]);
            if(wanted==null) { toAnswer = "noGroup"; return; }

            //группа 0, хеш 1
            Command response = new Command();
            response.type = "GroupDownload";
            response.arguments.Add(Cipher.encryption(JsonConvert.SerializeObject(wanted)));
            response.arguments.Add(wanted.hash());

            toAnswer = JsonConvert.SerializeObject(response);
        }


        #endregion

    }
}

