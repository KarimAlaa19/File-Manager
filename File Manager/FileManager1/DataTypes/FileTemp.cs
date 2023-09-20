using System.IO;
using System.Text;

namespace FileManager1
{
    public class FileTemp : IFileTemp
    {
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileDirectory {  get; set; }



        public bool CheckFileExists(string fileDirectory, string filename)
        {
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);

                return false;
            } else
            {
                return File.Exists(Path.Join($"{FileDirectory}", $"{filename}.{FileExtension}"));
            }
        }

        public void CreateFile()
        {
            if (CheckFileExists(FileDirectory, FileName))
            {
                Console.WriteLine("The File Already Exists!!!");
                return;
            }

            File.Create(Path.Join($"{FileDirectory}", $"{FileName}.{FileExtension}"));
        }


        public void DeleteFile(string fileName)
        {
            if (!CheckFileExists(FileDirectory, fileName))
            {
                Console.WriteLine("The File Doesn't Exist!!!");
                return;
            }

            File.Delete(Path.Join($"{FileDirectory}", $"{fileName}.{FileExtension}"));
            Console.WriteLine("Deleted Successfully");
        }

        public virtual string ReadFile(string fileName)
        {

            if (!CheckFileExists(FileDirectory, fileName))
            {

                return null;
            }
            else
            {
                StringBuilder resultData = new StringBuilder($"This Is a {FileExtension} File\n====>Content:\n");
                string[] dataLines = File.ReadAllLines(Path.Join($"{FileDirectory}", $"{fileName}.{FileExtension}"));
                Console.WriteLine(Path.Join($"{FileDirectory}", $"{FileName}.{FileExtension}"));

                foreach (string line in dataLines)
                {
                    resultData.AppendLine($"\n{line}");
                }
                return resultData.ToString();
            }
        }

        public List<string> GetAllFilesInDirectory(string fileDirectory)
        {
            List<string> files = new List<string>();

            if(Directory.Exists(fileDirectory))
            {
                foreach(string file in Directory.GetFiles(fileDirectory))
                {
                    files.Add(Path.GetFileNameWithoutExtension(file));
                }
            } 

            return files;
        }
    }
}
