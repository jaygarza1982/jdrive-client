using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OOD_Project_JD_Rive
{
    public class FileReader
    {
        public static byte[] readFile(string filePath)
        {
            //Wait until we can read file
            while (true)
            {
                try
                {
                    using (StreamReader stream = new StreamReader(filePath))
                    {
                        byte[] bytes = File.ReadAllBytes(filePath);
                        return bytes;
                    }
                }
                catch
                {
                    System.Threading.Thread.Sleep(50);
                }
            }
        }
    }
}