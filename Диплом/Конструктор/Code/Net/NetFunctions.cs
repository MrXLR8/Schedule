using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Share;
using Newtonsoft.Json;
namespace Builder
{
    public static class NetFunctions
    {
        public static string ip;
        public static int portNumber;

        public static bool compareSchedule(Schedule input)
        {
            

            string hash = input.hash();


            Command execute = new Command();
            execute.type = "ScheduleHashVerify";
            execute.arguments.Add(hash);

            Net.Initialize(ip, portNumber);

            string answer = Net.Send(JsonConvert.SerializeObject(execute));

            if (answer == "different") return true;
            else { return false; }
            
        }

        public static bool sendSchedule(Schedule input)
        {

            string json = JsonConvert.SerializeObject(input);

            Command execute = new Command();
            execute.type = "ScheduleUpload";
            execute.arguments.Add(Cipher.encryption(json));
            execute.arguments.Add(input.hash());

            Net.Initialize(ip, portNumber);

            string answer = Net.Send(JsonConvert.SerializeObject(execute));

            if (answer == "accepted") return true;
            else { return false; }

        }

        public static Schedule getSchedule()
        {
            Command execute = new Command();
            execute.type = "ScheduleDownload";
            Net.Initialize(ip, portNumber);

            string answer = Net.Send(JsonConvert.SerializeObject(execute));
            if (answer == "bad") return null;

            Command response;
            response = JsonConvert.DeserializeObject<Command>(answer);

            Schedule get = JsonConvert.DeserializeObject<Schedule>(Cipher.transcript(response.arguments[0]));

            if (get.hash() != response.arguments[1]) { return null; } //error

            return get;
        }
    }
}
