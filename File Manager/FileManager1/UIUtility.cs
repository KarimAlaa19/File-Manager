using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace FileManager1
{
    public class UIUtility
    {

        public static void PrintFileNames(List<string> files)
        {
            int i = 1;
            foreach (string file in files)
            {
                Console.WriteLine($"{i}- {file}");
                i++;
            }
        }

        public static string[] AppendStructureUI(FileTemp file)
        {
            List<string> data = new List<string>();
            string insertionLine;
            if(file.GetType().Name == typeof(ExcelFile).Name)
            {
                Console.WriteLine("Enter The Data You Want to Add, and When You are Done Type: \"=>\" in a new line");
                Console.WriteLine("Instructions: \n1. to represent a columns seperate each data on the same line with ','");
                Console.WriteLine("2. to represent a row just press enter to start a new line");
            }
            else
            {
                Console.WriteLine("Enter The Data You Want to Add, and When You are Done Type: \"=>\" in a new line");
            }

            do
            {
                insertionLine = Console.ReadLine();

                if (insertionLine == "=>")
                    break;

                data.Add(insertionLine);

            } while (true);

            return data.ToArray();
        }

        public static void SelectFileType(ref FileTemp file, ref bool uiFlag)
        {
            char fileTypeChoice = '0';

            Console.Clear();

            Console.WriteLine("Please Select File Type by Typing It's Number: ");
            Console.WriteLine("1- Word File \n2- PDF File \n3- Excel File \nOr Press any key to exit.");
            Console.Write("Enter Your Choice: ");
            fileTypeChoice = char.Parse(Console.ReadLine());


            switch (fileTypeChoice)
            {
                case '1':
                    file = new WordFile();
                    break;
                case '2':
                    file = new PDFFile();
                    break;
                case '3':
                    file = new ExcelFile();
                    break;
                default:
                    uiFlag = false;
                    Console.WriteLine("Thanks For Using Our App :D");
                    break;
            }
        }

        public static void ChooseFileIndex(ref int fileNumber, char operationChoice, List<string> files)
        {
            int number;

            Console.WriteLine("\t\t\t----------------------");

            if (files.Count <= 0) 
            {
                Console.WriteLine("There is no files added yet!!!");
                return;
            }

            
            PrintFileNames(files);

            Console.WriteLine("\t\t\t----------------------");

            Console.Write("Enter File Number: ");
            int.TryParse(Console.ReadLine(), out number);
            fileNumber = number - 1;

            Console.WriteLine("\t\t\t----------------------------\n");
        }

        public static bool IsValidFileIndex(List<string> files, int fileNumber)
        {
            return fileNumber < files.Count && fileNumber >= 0;
        }

        public static bool HanddleInvalidFileIndex(ref int fileNumber, char operationChoice, List<string> files)
        {
            ChooseFileIndex(ref fileNumber, operationChoice, files);

            if (!IsValidFileIndex(files, fileNumber))
            {
                Console.WriteLine("Invalid Index!!!");
                return true;
            }

            return false;
        }

        public static void PerformFileCreation(FileTemp file)
        {
            Console.Write("Enter File Name: ");
            file.FileName = Console.ReadLine();
            file.CreateFile();
        }

        public static void PerformAppend(FileTemp file, List<string> files, int fileNumber)
        {
            if (file.GetType().Name == typeof(ExcelFile).Name)
            {
                file.FileName = files[fileNumber];
                ((ExcelFile)file).AppendFile(AppendStructureUI(file));
            }
            else if (file.GetType().Name == typeof(WordFile).Name)
            {
                file.FileName = files[fileNumber];
                ((WordFile)file).AppendFile(AppendStructureUI(file));
            }
            else
            {
                Console.WriteLine("Not Allowed to Append This Type of Files!!!");
            }
        }

        public static void ChooseOperation(FileTemp file, List<string> files)
        {
            char operationChoice = '0';
            int fileNumber = 0;
            bool isNewOperationAvailable = true;

            do
            {
                Console.WriteLine("\t\t\t----------------------");

                Console.WriteLine("Please Select Operation Type by Typing It's Number: ");
                Console.WriteLine("1- Create File \n2- Read File \n3- Delete File \n4- Append File \nOr Press any key to exit.");
                Console.Write("Enter Your Choice: ");

                operationChoice = char.Parse(Console.ReadLine());


                switch (operationChoice)
                {
                    case '1':
                        PerformFileCreation(file);
                        break;
                    case '2':

                        if (HanddleInvalidFileIndex(ref fileNumber, operationChoice, files))
                            break;

                        Console.WriteLine(file.ReadFile(files[fileNumber]));
                        break;
                    case '3':
                        if (HanddleInvalidFileIndex(ref fileNumber, operationChoice, files))
                            break;

                        file.DeleteFile(files[fileNumber]);
                        break;
                    case '4':
                        if (HanddleInvalidFileIndex(ref fileNumber, operationChoice, files))
                            break;

                        PerformAppend(file, files, fileNumber);

                        break;
                    default:
                        break;
                }


                Console.WriteLine("Do You Want to Choose Another Operation: (y / n)");

                if (Console.ReadLine() != "y")
                    isNewOperationAvailable = false;

            } while (isNewOperationAvailable);
        }

        public static void RunFileManagerUI()
        {
            bool uiFlag = true;

            do
            {
                FileTemp file = null;

                SelectFileType(ref file, ref uiFlag);

                if (uiFlag)
                {
                    List<string> files = file.GetAllFilesInDirectory(file.FileDirectory);


                    ChooseOperation(file, files);

                    Console.WriteLine("\t\t\t----------------------------\n");

                    Console.Write("if you want to exit from the program (y / n): ");
                    if (Console.ReadLine().ToLower() == "y")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Thanks For Using Our App :D");
                        break;
                    }
                }

            } while (uiFlag);
        }
    }
}
