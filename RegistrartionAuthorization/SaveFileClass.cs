using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace RegistrartionAuthorization
{
    class SaveFileClass
    {
        public static void FileWriteLine(string Data, string FilePath)
        {
            using (StreamWriter sw = new StreamWriter(FilePath, true))
            {
                sw.WriteLine(Data);
            }
        }

        public static void FileReWrite(string Data, string FilePath)
        {
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                sw.WriteLine(Data);
            }
        }

        public static string FileRead(string FilePath)
        {
            string Data = "";

            using (StreamReader sr = new StreamReader(FilePath))
            {
                Data = sr.ReadLine();
            }

            return Data;
        }
    }
}
