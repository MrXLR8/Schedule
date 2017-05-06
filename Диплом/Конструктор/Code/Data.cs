using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public static class Data
    {

         public class Hash {

           private static MD5 md5Hash = MD5.Create();

            public static string GetMd5Hash( string input)
            {

                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }

            // Verify a hash against a string.
            public static bool VerifyMd5Hash(string input, string hash)
            {
                // Hash the input.
                string hashOfInput = GetMd5Hash(input);

                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void reset()
        {
            void clearWeek(Week input)
            {
                void clearDay(Day _input)
                {
                    _input.lectionList.Clear();
                }

                clearDay(input.Monday);
                clearDay(input.Tuesday);
                clearDay(input.Wednesday);
                clearDay(input.Thursday);
                clearDay(input.Friday);
                clearDay(input.Saturday);
            }

            Global.setEdits(null);

            Global.classList = new ObservableCollection<int>();

            Global.lectorList = new ObservableCollection<Lector>();
            Global.predmetList = new ObservableCollection<string>();
            Global.intervals.timeList = new ObservableCollection<Interval>();

            Global.classesWindow.Close();
            Global.predmetWindow.Close();
            Global.prepodWindow.Close();

            Global.commentaryWindow?.Close();
           // Global.commentaryWindow = null;

            Global.intervalWindow.Close();
            Global.intervalWindow = new IntervalsForm();

            foreach (Group c in Global.groupList)
            {
                clearWeek(c.chuslutel);
                clearWeek(c.znamenatel);
            }
            Global.groupList = new ObservableCollection<Group>();

            Global.selectedGroup = null;
            Global.selectedLection = null;
            // добавить сюда очищение ссылки на файл
        }


    }



    public class Schedule
    {
        public List<EncodedGroup> encodedGroups=new List<EncodedGroup>();
        public IntervalCollection intervals;

        public ObservableCollection<Lector> lectorList { get; set; }
        public  ObservableCollection<string> predmetList { get; set; }
        public  ObservableCollection<Group> groupList { get; set; }
        public  ObservableCollection<int> classList { get; set; }

        public DateTime modified;
        public string pcName;

        public string formJson() {

            foreach(Group g in Global.groupList)
            {
                encodedGroups.Add(new EncodedGroup(g));
            }

            intervals = Global.intervals;
            lectorList = Global.lectorList;
            predmetList = Global.predmetList;
            classList = Global.classList;

            modified = DateTime.Now;
            pcName = Environment.MachineName;


            return JsonConvert.SerializeObject(this);
        }

        public static Schedule getSchedule(string json)
        {
            return JsonConvert.DeserializeObject<Schedule>(json);
        }

        public void applyMe()
        {


            Data.reset();


            Global.intervals = intervals;
            Global.lectorList = lectorList;
            Global.predmetList = predmetList;
            Global.classList = classList;

            foreach(EncodedGroup eg in encodedGroups)
            {
                Global.groupList.Add(eg.getGroup());
            }
            try
            {
                Global.selectedGroup = Global.groupList[0];
                Global.selectedGroup.massReDraw();
            }
            catch (Exception e) { }
            Global.fixItemSource();
        }
    }

    public class EncodedGroup
    {
        public string json;
        public string hash;
        public EncodedGroup(Group input)
        {
            
            json = Cipher.encryption(JsonConvert.SerializeObject(input));

          hash = Data.Hash.GetMd5Hash(json);
        }

        public Group getGroup()
        {
            return JsonConvert.DeserializeObject<Group>(Cipher.transcript(json));
        }

       /* bool compare(string otherHash)
        {
            return Data.Hash.VerifyMd5Hash(hash, otherHash);
        }
        */
    }

}
