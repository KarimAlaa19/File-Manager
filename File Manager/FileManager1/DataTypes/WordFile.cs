using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager1
{
    public class WordFile : FileTemp, IWordFile
    {
        public WordFile()
        {
            FileExtension = "docx";
            FileDirectory = Path.Join(
                Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString(),
                "Files",
                "Word Files");
        }

        public void AppendFile(string[] dataLines)
        {
            if (!CheckFileExists(FileDirectory, FileName))
            {
                Console.WriteLine("The File Doesn't Exist!!!");
                return;
            }

            File.AppendAllLines(Path.Join($"{FileDirectory}", $"{FileName}.{FileExtension}"), dataLines);
        }
    }
}
