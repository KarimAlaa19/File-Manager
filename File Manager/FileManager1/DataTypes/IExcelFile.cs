using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager1
{
    public interface IExcelFile
    {
        void AppendFile(string[] data);
    }
}
