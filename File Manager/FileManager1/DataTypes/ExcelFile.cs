using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace FileManager1
{
    public class ExcelFile : FileTemp, IExcelFile
    {

        public ExcelFile()
        {
            FileExtension = "csv";
            FileDirectory = Path.Join(
                Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString(),
                "Files",
                "Excel Files");
        }

        public void AppendFile(string[] data)
        {
            if (CheckFileExists(FileDirectory, FileName))
            {
                StringBuilder dataPieces = new StringBuilder();

                foreach (string cell in data)
                {
                    dataPieces.Append($"{cell},");
                }

                dataPieces.Remove(dataPieces.Length - 1, 1);


                File.AppendAllLines(Path.Join($"{FileDirectory}", $"{FileName}.{FileExtension}"), data);
            }
            else
            {
                Console.WriteLine("You Have to Create The File First!!!");
            }
        }


        public override string ReadFile(string fileName)
        {
            if (!CheckFileExists(FileDirectory, fileName))
            {
                return null;
            }
            else
            {
                StringBuilder resultData = new StringBuilder($"This Is a {FileExtension} File\n====>Content: ");


                string[] dataLines = File.ReadAllLines(Path.Join($"{FileDirectory}", $"{fileName}.{FileExtension}"));
                foreach (string line in dataLines)
                {
                    resultData.Append($"\n{line.Replace(",", "\t")}");
                }

                return resultData.ToString();
            }
        }
    }
}
