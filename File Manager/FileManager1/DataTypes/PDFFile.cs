using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager1
{
    public class PDFFile : FileTemp
    {
        public PDFFile()
        {
            FileExtension = "pdf";
            FileDirectory = Path.Join(
                Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.ToString(),
                "Files",
                "PDF Files");
        }
    }
}
