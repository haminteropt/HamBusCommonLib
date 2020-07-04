using System;
using System.IO;

namespace CoreHambusCommonLibrary.DataLib
{
    public class BusInit
    {
        public string? DataFolder;
        public string hamBusDir = "HamBus";
        public BusInit()
        {
            CreateFolderIfNeeded();
        }
        public string GetLocalAppData()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return filePath;
        }

        public void CreateFolderIfNeeded()
        {
            try
            {
                DataFolder = GetLocalAppData() + "\\" + hamBusDir;

                if (File.Exists(DataFolder))
                    throw new Exception($"{hamBusDir} exist as a file.  Cannot continue");
                if (Directory.Exists(DataFolder))
                    return;
                Directory.CreateDirectory(DataFolder);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Create Data Directory Error: {e.Message}");
                throw e;
            }
        }
    }
}
