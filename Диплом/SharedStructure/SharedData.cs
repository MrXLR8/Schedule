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
    public static partial class Data
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




    }



    public partial class Schedule
    {
        public Schedule() {}
        public IntervalCollection intervals;

        public ObservableCollection<Lector> lectorList { get; set; }
        public  ObservableCollection<string> predmetList { get; set; }
        public  ObservableCollection<Group> groupList { get; set; }
        public  ObservableCollection<int> classList { get; set; }

      

        public DateTime modified;
        public string pcName;

        public string json() { return Cipher.encryption(JsonConvert.SerializeObject(this)); }

        public void fillMe(string _Encodedjson)
        {
            if (string.IsNullOrWhiteSpace(_Encodedjson)) throw new Exception("Файл не прочитан, или его содержимое пустое");
            string _json = Cipher.transcript(_Encodedjson);

            Schedule newone =JsonConvert.DeserializeObject<Schedule>(_json);

            intervals = newone.intervals;
            lectorList = newone.lectorList;
            predmetList = newone.predmetList;
            groupList = newone.groupList;
            classList = newone.classList;
        }


    }

  

}
