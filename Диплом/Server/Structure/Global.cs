using Newtonsoft.Json;
using Server;
using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
