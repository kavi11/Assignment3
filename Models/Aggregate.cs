using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSV.Models
{
    class Aggregate
    {
        public static void Functions(List<Student> ls)
        {
            Console.WriteLine("Total Count of items in the List - " + ls.Count);
            Console.Write("\n");

            List<int> age_ls = new List<int>();
            List<Student> start_ls = new List<Student>();
            List<Student> contain_ls = new List<Student>();

            int startwith_count = 0;
            int contains_count = 0;
            foreach (var student in ls)
            {
                int age = student.Age;
                Boolean mr = student.MyRecord;
                age_ls.Add(age);
                String enter_character = "K";

                if (student.FirstName.StartsWith(enter_character))
                {
                    startwith_count++;
                    start_ls.Add(student);
                }


                if (student.FirstName.Contains(enter_character))
                {
                    contains_count++;
                    contain_ls.Add(student);
                }

                if (mr == true)
                {
                    Console.WriteLine("My Record is - " + student);
                    Console.Write("\n");
                }
            }
            foreach (var student in start_ls)
            {
                Console.Write("Name of Students Start with K - " + student + "  \n");
            }
            Console.Write("\n");
            foreach (var student in contain_ls)
            {
                Console.Write("Name of Students Containing K - " + student + "  \n");
            }

            double average = age_ls.Count > 0 ? age_ls.Average() : 0;
            int min = age_ls.Count > 0 ? age_ls.Min() : 0;
            int max = age_ls.Count > 0 ? age_ls.Max() : 0;

            Console.Write("\n");
            Console.WriteLine("Total Number of Student Name Starts with K are - " + start_ls.Count);
            Console.Write("\n");
            Console.WriteLine("Total Number of Student Name Containing K's are - " + contain_ls.Count);
            Console.Write("\n");
            Console.WriteLine("Average Age - " + (int)Math.Round(average));
            Console.Write("\n");
            Console.WriteLine("Minimun Age - " + min);
            Console.Write("\n");
            Console.WriteLine("Maximum Age - " + max);
            Console.Write("\n");
        }

    }
}
