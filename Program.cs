using System;
using System.Collections.Generic;
using System.Reflection;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    class Program 
    {
        static void Main() 
        {
            List<Student> listOfStudents = new List<Student> { };
            int keepGoing;
            do
            {
                listOfStudents.Add(GetStudentInfoFromUser());
                Console.WriteLine("Press 1 to enter another student record. Press any other key to exit the program");
                _ = int.TryParse(Console.ReadLine(), out keepGoing);
            } while (keepGoing == 1);

            listOfStudents.ForEach(s => PrintRecord(s));
        }
        static Student GetStudentInfoFromUser()
        {
            Console.WriteLine("Enter Your student ID number: ");
            _ = int.TryParse(Console.ReadLine(), out int studentId);

            Console.WriteLine("Enter your first name: ");
            string studentFirstName = Console.ReadLine();

            Console.WriteLine("Enter your last name: ");
            string studentLastName = Console.ReadLine();

            Console.WriteLine("Enter the class you want to attend: ");
            string className = Console.ReadLine();

            Console.WriteLine("Enter last last class you completed: ");
            string lastClass = Console.ReadLine();

            Console.WriteLine("When did you complete this class? Enter the date in format MM/dd/YYYY: ");
            _ = DateTimeOffset.TryParse(Console.ReadLine(), out DateTimeOffset lastCompletedOn);

            Console.WriteLine("Enter the date you wish to start on, in format MM/dd/YYYY: ");
            _ = DateTimeOffset.TryParse(Console.ReadLine(), out DateTimeOffset startDate);

            return new Student
            {
                StudentId = studentId,
                FirstName = studentFirstName,
                LastName = studentLastName,
                ClassName = className,
                StartDate = startDate,
                LastClassCompleted = lastClass,
                LastClassCompletedOn = lastCompletedOn
            };
        }
        static void PrintRecord<T>(T record)
        {
            PropertyInfo[] objectInfo = record.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var propInfo in objectInfo) 
                Console.WriteLine($"{propInfo.Name}: \t{propInfo.GetValue(record, null)}");

            Console.WriteLine();
        }
    }
}