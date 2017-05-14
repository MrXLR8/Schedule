using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Share;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Builder
{
    public static class NetFunctions
    {
        public static string ip;
        public static int portNumber;

         #region Schedule
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

        #endregion schedule

        #region group
        public static bool compareGroup(Group input)
        {

            string hash = input.hash();

            Command execute = new Command();

            execute.type = "GroupHashVerify";
            execute.arguments.Add(input.name);
            execute.arguments.Add(hash);

            Net.Initialize(ip, portNumber);

            string answer = Net.Send(JsonConvert.SerializeObject(execute));

            if (answer == "different") return true;
            else { return false; }

        }

        public static List<String> GroupListDownload()
        {
            Command execute = new Command();

            execute.type = "GroupListDownload";

            Net.Initialize(ip, portNumber);

            string json = Net.Send(JsonConvert.SerializeObject(execute));
            try
            {
                List<string> result = JsonConvert.DeserializeObject<List<string>>(json);
                return result;
            }
            catch (Exception) { return null; }
            
        }

        public static Group GroupDownload(string groupName)
        {
            Command execute = new Command();

            execute.type = "GroupDownload";
            execute.arguments.Add(groupName);

            Net.Initialize(ip, portNumber);
            try
            {
                string json = Net.Send(JsonConvert.SerializeObject(execute));
                
                Command response = JsonConvert.DeserializeObject<Command>(json);
                string decoded = Cipher.transcript(response.arguments[0]);
                Group result = JsonConvert.DeserializeObject<Group>(decoded);
                if (result.hash() == response.arguments[1]) {
                    Global.intervals.timeList = JsonConvert.DeserializeObject<ObservableCollection<Interval>>(response.arguments[2]);
                    return result;
                }

                return null;
            }
            catch (Exception) { return null; }

        }
        #endregion

    }
}
