using System;
using System.Collections.Generic;
using System.Text;
using Builder;
using Newtonsoft.Json;
using Server;
using System.Net;

namespace Share
{
    public partial class Command
    {
        const string LOGTYPE = "SRV";
        [JsonIgnore]
        IPAddress ip;
        public void qualify(IPAddress _ip)
        {
            ip = _ip;
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

                case "GroupHashVerify":
                    GroupHashVerify();
                    return;
                case "GroupListDownload":
                    GroupListDownload();
                    return;
                case "GroupDownload":
                    GroupDownload();
                    return;


                default:
                    toAnswer = "bad";
                    return;
            }
        }

        
        bool checkEmpty()
        {
            if (Global.MainSchedule == null || Global.MainSchedule.groupList.Count == 0)
            {
                toAnswer = "empty";
                Log.write(LOGTYPE,ip,"На сервере на загруженно рабочие расписание, или в нем отсуствуют группы!", ConsoleColor.Red);
                return true; }
            return false;
        }

        #region schedule

                public void ScheduleHashVerify()
                {
                     Log.write(LOGTYPE, ip, "Запросил сравнение актуальности расписаний");

                 if (checkEmpty()) { toAnswer = "different"; return; }


                    string myHash = Global.MainSchedule.hash();
                    if (myHash == arguments[0]) { toAnswer = "same";
                    Log.write(LOGTYPE, ip, "Полученное расписания идентично с серверным");
            }
                    else { toAnswer = "different";
                Log.write(LOGTYPE, ip, "Полученное расписания отличается от серверного");
            }


            
                }

                 public void ScheduleUpload()
                {
                     Log.write(LOGTYPE, ip, "Загружает свое расписание");
                    Schedule recived;
                    try
                    {
                        string decrypted = Cipher.transcript(arguments[0]);
                        recived = JsonConvert.DeserializeObject<Schedule>(decrypted);
                        if (recived == null || recived.groupList.Count == 0) throw new Exception();
                        if(recived.hash()!=arguments[1]) throw new Exception();
                    }
                    catch (Exception e) {
                         Log.write(LOGTYPE, ip, "Расписание не принято. Полученно некорректно, или не содержит групп",ConsoleColor.Red);

                         toAnswer = "bad";
                        return;
                    }

                    Global.MainSchedule = recived;
                    Log.write(LOGTYPE, ip, "Установленно новое расписание. Количество групп: " + recived.groupList.Count,ConsoleColor.Green);
                    Log.write(LOGTYPE, "Создано на ПК :" + recived.pcName,ConsoleColor.Green);
                    toAnswer = "accepted";

                }

                public void ScheduleDownload()
                {
                     Log.write(LOGTYPE, ip, "Запросил загрузку расписания с сервера");
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
                     Log.write(LOGTYPE, ip, "Расписание не удалось отправить!",ConsoleColor.Red);
                    toAnswer = "bad";
                        return;
                    }

                Log.write(LOGTYPE, ip, "Расписание переданно клиенту",ConsoleColor.Green);


                }

        #endregion


        #region group

        public void GroupHashVerify()
        {
            // группа 0. хеш 1
            Log.write(LOGTYPE, ip, "Запросил сравнение актуальности группы "+arguments[0],ConsoleColor.Gray);

            if(checkEmpty()) {  return; }


            Group inside = Global.getGroup(Global.MainSchedule,arguments[0]);

            if (inside == null)
            {
                toAnswer = "noGroup";
                Log.write(LOGTYPE, ip,string.Format("Группа ({0}) не найдена среди {1} групп", arguments[0], Global.MainSchedule.groupList.Count),ConsoleColor.Red);
            }


            string myHash = inside.hash();
            if (myHash == arguments[1]) {
                toAnswer = "same";
                Log.write(LOGTYPE, ip,"Группа "+ arguments[0]+" одинакова на обоих сторонах", ConsoleColor.Gray);
            }
            else {
                toAnswer = "different";
                Log.write(LOGTYPE, ip,"Группа " + arguments[0] + " одинакова на обеих сторонах");
            }


        }

        public void GroupListDownload()
        {
            Log.write(LOGTYPE, ip + "Запросил список всех групп", ConsoleColor.Gray);

            if (checkEmpty()) {   return; }

            List<string> groups = new List<string>();

            foreach(Group g in Global.MainSchedule.groupList)
            {
                groups.Add(g.name);
            }

            string json = JsonConvert.SerializeObject(groups);

            toAnswer = json;
            Log.write(LOGTYPE, ip, "Переслан список из " +groups.Count+ " групп для клиента", ConsoleColor.Gray);
        }

        public void GroupDownload()
        {
            Log.write(LOGTYPE, ip,"Запросил загрузку группы: " + arguments[0], ConsoleColor.Gray);

            if (checkEmpty()) { return; }

            Group wanted = Global.getGroup(Global.MainSchedule, arguments[0]);
            if(wanted==null) { toAnswer = "noGroup"; Log.write(LOGTYPE, ip,string.Format("Группа ({0}) не найдена среди {1} групп", arguments[0], Global.MainSchedule.groupList.Count), ConsoleColor.Red); return; }

            //группа 0, хеш 1
            Command response = new Command();
            response.type = "GroupDownload";
            response.arguments.Add(Cipher.encryption(JsonConvert.SerializeObject(wanted)));
            response.arguments.Add(wanted.hash());
            response.arguments.Add(JsonConvert.SerializeObject(Global.MainSchedule.intervals.timeList));

           toAnswer = JsonConvert.SerializeObject(response);
        }


        #endregion

    }
}

