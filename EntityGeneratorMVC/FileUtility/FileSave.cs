using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGeneratorMVC.FileUtility
{
    internal static class FileSave
    {
        public static void SaveFileInDirectoryModel(string FilePath, string FileName, string Content)
        {
            string dirFilePath = FilePath + "\\Model";
            string saveFilePath = dirFilePath + "\\" + FileName;
            if (!Directory.Exists(dirFilePath))
            {
                Directory.CreateDirectory(dirFilePath);
            }
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
            }
            TextWriter tw = File.CreateText(saveFilePath);
            tw.WriteLine(Content);
            tw.Close();

        }
        public static void SaveFileInDirectoryBussinessLogic(string FilePath, string FileName, string Content)
        {
            string dirFilePath = FilePath + "\\Service";
            string saveFilePath = dirFilePath + "\\" + FileName;
            if (!Directory.Exists(dirFilePath))
            {
                Directory.CreateDirectory(dirFilePath);
            }
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
            }
            TextWriter tw = File.CreateText(saveFilePath);
            tw.WriteLine(Content);
            tw.Close();

        }
        public static void SaveFileInDirectoryContext(string FilePath, string FileName, string Content)
        {
            string saveFilePath = FilePath + "\\" + FileName;
            
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
            }
            TextWriter tw = File.CreateText(saveFilePath);
            tw.WriteLine(Content);
            tw.Close();

        }
    }
}
