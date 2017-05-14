 using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
   public static class FileInteraction
    {
        public static string filePath;
        public static string fileName;

        private static SaveFileDialog saveFileDialog=new SaveFileDialog();

        private static OpenFileDialog openFileDialog=new OpenFileDialog();

        public static bool saveToFile(string toSave)
        {
            try
            {
                saveFileDialog.Filter = "Файл расписания | *.schd";
                saveFileDialog.DefaultExt = "schd";
                if (saveFileDialog.ShowDialog() == true)
                {
                    fileName = System.IO.Path.GetFileName(saveFileDialog.FileName);
                    filePath = saveFileDialog.FileName;
                    File.WriteAllText(filePath, toSave);

                    Global.setSaveButton();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                filePath = "";
                fileName = "";
                Global.setSaveButton();
                return false;
            }
        }

        public static bool saveToFile(string toSave, string path)
        {
            try
            {

                    fileName = System.IO.Path.GetFileName(path);
                    filePath = saveFileDialog.FileName;
                    File.WriteAllText(path, toSave);

                    Global.setSaveButton();
                    return true;
            }
            catch (Exception)
            {
                filePath = "";
                fileName = "";
                Global.setSaveButton();
                return false;
            }
        }

        public static bool saveToSavedPath(string toSave)
        {
            try
            {
                File.WriteAllText(filePath, toSave);

                Global.setSaveButton();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
            }

        public static string openFile()
        {
            try
            {
                openFileDialog.Filter = "Файл расписания | *.schd";
                openFileDialog.DefaultExt = "schd";
                if (openFileDialog.ShowDialog() == true)
                {
                    
                    filePath =openFileDialog.FileName;
                    fileName = System.IO.Path.GetFileName(openFileDialog.FileName);
                    Global.setSaveButton();
                    return File.ReadAllText(filePath);


                }
                Global.setSaveButton();
                return "";
            }
            catch (Exception)
            {
                filePath = "";
                fileName = "";
                Global.setSaveButton();
                return "";
            }
        }


        public static string openFile(string path)
        {
            try
            {

                     filePath = path;
                    fileName = System.IO.Path.GetFileName(path);
                    Global.setSaveButton();
                    return File.ReadAllText(filePath);

            }
            catch (Exception)
            {
                filePath = "";
                fileName = "";
                Global.setSaveButton();
                return "";
            }
        }

        public struct NetSettings
        {
            public string ip;
            public int port;
        }

        public static void saveNetSettings()
        {
            NetSettings toSave = new NetSettings();
            toSave.ip = NetFunctions.ip;
            toSave.port = NetFunctions.portNumber;
            string json = JsonConvert.SerializeObject(toSave);
            try
            {
                File.WriteAllText("netSettings.json", json);
            }
            catch(Exception) {}
        }

        public static void loadNetSettings()
        {
            string json = File.ReadAllText("netSettings.json");
            NetSettings loaded= JsonConvert.DeserializeObject<NetSettings>(json); 

            NetFunctions.ip = loaded.ip;
            NetFunctions.portNumber = loaded.port;

            string[] ip = loaded.ip.Split('.');

            Global.syncForm.ip1.Text = ip[0];
            Global.syncGroupForm.ip1.Text = ip[0];

            Global.syncForm.ip2.Text = ip[1];
            Global.syncGroupForm.ip2.Text = ip[1];

            Global.syncForm.ip3.Text = ip[2];
            Global.syncGroupForm.ip3.Text = ip[2];

            Global.syncForm.ip4.Text = ip[3];
            Global.syncGroupForm.ip4.Text = ip[3];

            Global.syncForm.port.Text = loaded.port.ToString();
            Global.syncGroupForm.port.Text = loaded.port.ToString();

            Global.syncForm.Check.IsEnabled = true;
            Global.syncGroupForm.RefreshGroups.IsEnabled = true;

        }

    }
}
