using Newtonsoft.Json;
using Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Builder
{
   public static class Global
    {
        public static Schedule MainSchedule;

        public static List<IPAddress> blacklist=new List<IPAddress>();
        public static List<IPAddress> whitelist = new List<IPAddress>();


        public static Group getGroup(Schedule input,string groupName)
        {
            foreach(Group g in MainSchedule.groupList)
            {
                if (g.name == groupName) return g;
            }
            return null;
        }


        public static void SaveSchedule(Schedule input)
        {
            try
            {
                Global.MainSchedule = input;
                File.WriteAllText("last.schd", MainSchedule.json());
                Log.write("FILE", "Расписание сохраненно в файл", ConsoleColor.Green);
            }
            catch(Exception exc)
            {
                Log.write("FILE", "Не удалось записать новое расписание в файл last.schd", ConsoleColor.Red);
            }

        }

        public static void LoadSchedule(string filename)
        {
            try
            {
                string read = File.ReadAllText(filename);
                Global.MainSchedule = JsonConvert.DeserializeObject<Schedule>(Cipher.transcript(read));
                Log.write("FILE", "Расписание успешно загружено с файла: " + filename, ConsoleColor.Green);

            }
            catch (Exception exc)
            {
                Log.write("FILE", "Не удалось загрузить файл: " +filename, ConsoleColor.Red);
            }

        }



        public static bool isInList(List<IPAddress> list, IPAddress ip)
        {
            foreach (IPAddress addr in list)
            {
                if (addr.ToString() == ip.ToString()) return true;
            }
            return false;
        }

        public static void listAdd(List<IPAddress> list,IPAddress ip)
        {
            if (isInList(list,ip)) return;

            list.Add(ip);
        }

        public static bool listRemove(List<IPAddress> list, IPAddress ip)
        {
            foreach (IPAddress addr in list)
            {
                if (addr.ToString() == ip.ToString()) { list.Remove(addr); return true; } 
            }
            return false;
        }

        public static List<IPAddress> LoadList(string filename)
        {
            List<IPAddress> result=new List<IPAddress>();
            try
            {
                string read = File.ReadAllText(filename);

                List<string> readO = JsonConvert.DeserializeObject<List<string>>(read);
                foreach(string s in readO)
                {
                        result.Add(IPAddress.Parse(s));
 
                }
                return result;
            }
            catch (Exception exc)
            {
                Log.write("FILE", "Не прочитать файл: " + filename, ConsoleColor.Red);
                return new List<IPAddress>();
            }

        }

        public static void SaveList(List<IPAddress> list, string filename)
        {
            List<string> towrite = new List<string>();
            foreach(IPAddress addr in list)
            {
                towrite.Add(addr.ToString());
            }
            try
            {
                string json = JsonConvert.SerializeObject(towrite);
                File.WriteAllText(filename, json);
            }
            catch (Exception exc)
            {
                Log.write("FILE", "Не удалось записать в файл "+filename, ConsoleColor.Red);
            }

        }
    }
}
