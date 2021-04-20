using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{


    class Start
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();







            int case_Switch;
            do
            {
                Console.WriteLine("1. Add a student ");
                Console.WriteLine("2. Show students table ");
                Console.WriteLine("3. Add a student with random grades and exam results ");
                Console.WriteLine("4. Add a student(s) from a .txt file ");
                Console.WriteLine("5. Clear the screen. ");
                Console.WriteLine("6. Exit the program. ");
                Console.WriteLine("");
                Console.Write("What would You like to do? ");
                case_Switch = int.Parse(Console.ReadLine());
                switch (case_Switch)
                {
                    case 1:
                        students.Add(Student.create_Student());
                        Console.WriteLine("\n");
                        break;

                    case 2:
                        Student.formated_Print(students);
                        Console.WriteLine("\n");
                        break;
                    case 3:
                        students.Add(Student.create_Student(true));
                        Console.WriteLine("\n");
                        break;
                    case 4:

                        students = File.ReadAllLines("C:\\Users\\justa\\source\\repos\\ConsoleApp1\\ConsoleApp1\\students.txt").Skip(1).Select(Student.from_Txt).ToList();
                        students.RemoveAll(r => r.Name == "0");

                        Console.WriteLine("Added {0} student(s) from the file", students.Count());
                        Console.WriteLine("\n");
                        break;
                    case 5:
                        Console.Clear();
                        break;
                    case 6:
                        break;

                }
            } while (case_Switch != 6);


        }

        public static Boolean if_str_contain_int(string str)
        {
            bool containsInt = str.Any(char.IsDigit);
            if (containsInt)
            {
                return true;
            }

            else return false;
        }
    }
}
