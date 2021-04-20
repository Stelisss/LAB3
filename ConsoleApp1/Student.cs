using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Student
    {

        private string name;
        private string surname;
        private List<int> hw;
        private int exam;

        static private Random rand = new Random();

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public List<int> Hw { get => hw; set => hw = value; }
        public int Exam { get => exam; set => exam = value; }

        public Student() { }

        public Student(string Name, string Surname, List<int> Hw, int Exam)
        {
            name = Name;
            surname = Surname;
            hw = Hw; ;
            exam = Exam;
            }

        public static Student create_Student(bool if_random = false)
        {


            Student student = new Student();


        create_Name:
            Console.Write("Enter the students name: ");
            string name = Console.ReadLine();
            //checking if entered name is not empty and that it doesnt contain integer values
            if (!String.IsNullOrEmpty(name) && !Start.if_str_contain_int(name))
            {
                student.name = name;
                goto create_Surname;
            }
            else
            {
                Console.WriteLine("Bad input");
                goto create_Name;
            }

        create_Surname:
            Console.Write("Enter the students surname: ");
            string surname = Console.ReadLine();
            //checking if entered surname is not empty and that it doesnt contain integer values
            if (!String.IsNullOrEmpty(surname) && !Start.if_str_contain_int(surname))
            {
                student.surname = surname;
                goto create_HW;
            }
            else
            {
                Console.WriteLine("Bad input");
                goto create_Surname;
            }

        create_HW:
            student.hw = create_HW_list(if_random);
            goto create_Exam;

        create_Exam:
            try
            {
                if (if_random == true)
                {
                    student.exam = get_random_exam();
                }
                else
                {
                    Console.Write("Enter the students exam result: ");
                    int exam = Convert.ToInt32(Console.ReadLine());
                    if (exam == 0)
                    {
                        Console.WriteLine("Exam result can't be 0 ");
                        goto create_Exam;
                    }

                    // checking if entered exam is between 1 and 10
                    if (range_check(exam) == true)
                    {
                        student.exam = exam;
                    }
                    else
                    {
                        Console.WriteLine("Exam should be between 1 and 10.");
                        goto create_Exam;
                    }

                }

            }
            catch (FormatException e)
            {
                Console.WriteLine("No input was given. Exam grade must be between 1 and 10");
                goto create_Exam;
            }
            goto End;

        End:
            return student;
        }



        public static List<int> create_HW_list(bool if_random)
        {
            List<int> HW = new List<int>();
            int grade;
            bool cycle = true;
            if (if_random == true)
            {

                return get_random_hw_list();

            }
            else
            {
                while (cycle)
                {
                    try
                    {

                        Console.Write("Enter students grade (0 to stop entering grades): ");

                        grade = Convert.ToInt32(Console.ReadLine());
                        // cheecking if entered 0, if yes stop entering homework
                        if (grade == 0)
                        {
                            //checking if entered amount of homework is 0, if yes let enter more. Homework amount cant be 0 
                            if (HW.Count() == 0)
                            {
                                Console.WriteLine("No student homework(s) entered. Student must have at least one homework.");
                                continue;
                            }
                            else
                            {
                                cycle = false;
                                break;
                            }
                        }

                        // checking if entered grade is between 1 and 10
                        if (range_check(grade) == true)
                        {
                            HW.Add(grade);
                        }
                        else
                        {
                            Console.WriteLine("Grade should be between 1 and 10.");
                        }

                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("No input was given. Enter a homework grade between 1 and 10");
                        continue;
                    }

                }

            }
            return HW;

        }

        public static Boolean range_check(int a)
        {
            if (Enumerable.Range(Constants.MIN, Constants.MAX).Contains(a))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static double get_Average(List<int> homeworks)
        {
            double total_grades = 0;
            foreach (var homework in homeworks)
            {
                total_grades += homework;
            }
            return total_grades / homeworks.Count();
        }

        public static double get_Median(List<int> homeworks)
        {
            int[] intList = homeworks.ToArray();


            if (intList.Length % 2 == 0)
            {
                //finding two middle elements 
                double a = intList[intList.Length / 2 - 1];
                double b = intList[intList.Length / 2];
                return (a + b) / 2;
            }
            else
            {
                return intList[intList.Length / 2];
            }
        }

        public static void formated_Print(List<Student> students)
        {
            List<Student> sorted_Students = new List<Student>();
            sorted_Students = students.OrderBy(student => student.Surname).ToList();

            //checking if student list is empty, if yes throw error message, if no print the table.
            if (sorted_Students.Count == 0)
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                Console.WriteLine("\n");
                Console.WriteLine("{0, -15} {1, -23} {2, -28} {3, -33}", "Surname", "Name", "Final points (Avg.)", "Final points (Med.)");

                for (int i = 0; i < 88; i++)
                {
                    Console.Write("-");
                }

                Console.WriteLine("\n");

                foreach (var student in sorted_Students)
                {
                    Console.WriteLine("{0, -15} {1, -23} {2, -28} {3, -33}", student.Surname, student.Name, String.Format("{0:0.##}", 0.3 * Student.get_Average(student.Hw) + 0.7 * student.Exam), String.Format("{0:0.##}", 0.3 * Student.get_Median(student.Hw) + 0.7 * student.Exam));
                }

            }


        }

        public static Student from_Txt(string txt_Line)
        {
            try
            {
                string[] values = txt_Line.Split(" ");
                Student student = new Student();
                List<int> grades = new List<int>();
                student.name = values[0];
                student.surname = values[1];
                //hardcoded, so if getting data from file, student must have 5 homework
                grades.Add(Convert.ToInt32(values[2]));
                grades.Add(Convert.ToInt32(values[3]));
                grades.Add(Convert.ToInt32(values[4]));
                grades.Add(Convert.ToInt32(values[5]));
                grades.Add(Convert.ToInt32(values[6]));
                student.hw = grades;

                student.exam = Convert.ToInt32(values[7]);
                return student;

            }
            catch (FormatException e)
            {
                Console.WriteLine("There was a problem with the format of the data in the file");
                Student student = new Student();
                List<int> grades = new List<int>();
                student.name = "0";
                student.surname = "0";
                grades.Add(0);
                student.hw = grades;
                student.exam = 0;
                return student;

            }

        }

        public static void random_Generation(int amount)
        {

            List<Student> students = new List<Student>();

            for (int i = 0; i < amount; i++)
            {
                Student student = new Student();
                student.name = get_random_name(amount);
                student.surname = get_random_surname(amount);
                student.hw = get_random_hw_list();
                student.exam = get_random_exam();
                students.Add(student);
            }
            Write_to_csv(students, amount);


        }


        public static void Write_to_csv(List<Student> students, int amount)
        {
            Stopwatch timer = new Stopwatch();
            timer.Restart();

            List<Student> sorted_Students = new List<Student>();
            sorted_Students = students.OrderBy(student => student.Surname).ToList();

            var failed_txt = new StringBuilder();
            var passed_txt = new StringBuilder();

            failed_txt.Append(Constants.HEADER + "\n");
            passed_txt.Append(Constants.HEADER + "\n");

            foreach (Student student in sorted_Students)
            {
                if ((0.3 * Student.get_Average(student.Hw) + 0.7 * student.Exam) < 5)
                {
                    var newline = string.Format("{1} {0} {2}", student.Name, student.Surname, String.Format("{0:0.##}", 0.3 * Student.get_Average(student.Hw) + 0.7 * student.Exam));
                    failed_txt.AppendLine(newline);

                }
                else
                {
                    var newline = string.Format("{1} {0} {2}", student.Name, student.Surname, String.Format("{0:0.##}", 0.3 * Student.get_Average(student.Hw) + 0.7 * student.Exam));
                    passed_txt.AppendLine(newline);
                }
            }

            File.WriteAllText("Failed_students" + amount + ".txt", failed_txt.ToString());
            File.WriteAllText("Passed_students" + amount + ".txt", passed_txt.ToString());

            timer.Stop();
            Console.WriteLine("Time elapsed to generate {0} students: {1}\n", amount, timer.Elapsed);
        }

        public static string get_random_name(int amount)
        {
            return "Name" + rand.Next(amount);
        }


        public static string get_random_surname(int amount)
        {
            return "Surname" + rand.Next(amount);
        }

        public static List<int> get_random_hw_list()
        {
            List<int> HW = new List<int>();
            int rand_cycle = rand.Next(Constants.MIN, Constants.MAX);
            for (int i = 0; i <= rand_cycle; i++)
            {
                HW.Add(rand.Next(Constants.MIN, Constants.MAX));
            }
            return HW;
        }

        public static int get_random_exam()
        {
            return rand.Next(Constants.MIN, Constants.MAX);
        }




    }
    
    }



