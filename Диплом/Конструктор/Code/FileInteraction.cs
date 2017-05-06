using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            catch (Exception e)
            {
                filePath = "";
                fileName = "";
                Global.setSaveButton();
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
            catch (Exception e)
            {
                filePath = "";
                fileName = "";
                Global.setSaveButton();
                return "";
            }
        }

    }
}
