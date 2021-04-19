using System;
using System.Collections.Generic;
using System.Linq;

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


        public static Student create_Student(bool if_random = false)
        {
            Student student = new Student();


            create_Name:
                Console.Write("Enter the students name: ");
                string name = Console.ReadLine();
                //checking if entered name is not empty and that it doesnt contain integer values
                if (!String.IsNullOrEmpty(name) && !Program.if_str_contain_int(name))
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
                if (!String.IsNullOrEmpty(surname) && !Program.if_str_contain_int(surname))
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
                    student.exam = rand.Next(Constants.MIN, Constants.MAX);
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
                    
                } catch (Exception e)
                {
                    Console.WriteLine(e);
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
                int rand_cycle = rand.Next(Constants.MIN, Constants.MAX);
                for (int i = 0; i <= rand_cycle; i++)
                {
                    HW.Add(rand.Next(Constants.MIN, Constants.MAX));
                }
                return HW;

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
                    //!!! neet to fix exception, make message more clear

                } catch (Exception e)
                {
                    Console.WriteLine(e);
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

        public static void formated_Print(List<Student> students, string med_or_avg)
        {
            if(med_or_avg == "average")
            {
                Console.WriteLine("\n");
                Console.WriteLine("{0, -15} {1, -23} {2, -28}", "Surname", "Name", "Final points (Avg.)");

                for(int i = 0; i < 59; i++)
                {
                    Console.Write("-");
                }

                Console.WriteLine("\n");

                foreach (var student in students)
                {
                    Console.WriteLine("{0, -15} {1, -23} {2, -28}", student.Surname, student.Name, String.Format("{0:0.##}", 0.3 * Student.get_Average(student.Hw) + 0.7));
                }
            }
            else if(med_or_avg == "median")
            {
                Console.WriteLine("\n");
                Console.WriteLine("{0, -15} {1, -23} {2, -28}", "Surname", "Name", "Final points (Med.)");

                for (int i = 0; i < 59; i++)
                {
                    Console.Write("-");
                }

                Console.WriteLine("\n");

                foreach (var student in students)
                {
                    Console.WriteLine("{0, -15} {1, -23} {2, -28}", student.Surname, student.Name, String.Format("{0:0.##}", 0.3 * Student.get_Median(student.Hw) + 0.7));
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();







            int case_Switch;
            do
            {
                Console.WriteLine("1. Add a student ");
                Console.WriteLine("2. Show Final Points (calculated using average) ");
                Console.WriteLine("3. Show Final Points (calculated using median) ");
                Console.WriteLine("4. Add a student with random grades and exam results ");
                Console.WriteLine("5. Clear the screen. ");
                Console.WriteLine("6. Exit the program. ");
                Console.WriteLine("");
                Console.Write("What would You like to do? ");
                case_Switch = int.Parse(Console.ReadLine());
                switch (case_Switch)
                {
                    case 1:
                        students.Add(Student.create_Student());
                        break;

                    case 2:
                        Student.formated_Print(students, "average");
                        break;
                    case 3:
                        Student.formated_Print(students, "median");
                        break;
                    case 4:
                        students.Add(Student.create_Student(true));
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
