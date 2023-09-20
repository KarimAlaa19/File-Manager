using System.IO;

namespace FileManager1
{
    interface IFileTemp
    {
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileDirectory { get; set; }



        bool CheckFileExists(string fileDirectory, string filename);
        void CreateFile();
        void DeleteFile(string fileName);
        string ReadFile(string fileName);
        List<string> GetAllFilesInDirectory(string fileDirectory);
    }
}
