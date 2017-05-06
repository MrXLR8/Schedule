using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            Global.classList.Clear();
            Global.groupList.Clear();
            Global.lectorList.Clear();
            Global.predmetList.Clear();
            Global.intervals.timeList.Clear();

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

            Global.selectedGroup = null;
            Global.selectedLection = null;
            // добавить сюда очищение ссылки на файл
        }


    }



    public class Schedule
    {
        public string encodedData;
        public string hash;

        public DateTime modified;
        public string pcName;

    }

    public class EncodedGroup
    {
        

        string json;
        string hash;
        public EncodedGroup(Group input)
        {
          json= JsonConvert.SerializeObject(input);
            hash = Data.Hash.GetMd5Hash(json);
        }
    }

}
