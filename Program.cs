using System;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter Student Id");
            int studentId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter First Name");
            string studentFirstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name");
            string studentLastName = Console.ReadLine();
            Console.WriteLine("Enter Class Name");
            string className = Console.ReadLine();
            Console.WriteLine("Enter Last Class Completed");
            string lastClass = Console.ReadLine();
            Console.WriteLine("Enter Last Class Completed Date in format MM/dd/YYYY");
            DateTimeOffset lastCompletedOn = DateTimeOffset.Parse(Console.ReadLine());
            Console.WriteLine("Enter Start Date in format MM/dd/YYYY");
            DateTimeOffset startDate = DateTimeOffset.Parse(Console.ReadLine());

            Student studentRecord = new Student
            {
                StudentId = studentId,
                FirstName = studentFirstName,
                LastName = studentLastName,
                ClassName = className,
                StartDate = startDate,
                LastClassCompleted = lastClass,
                LastClassCompletedOn = lastCompletedOn
            };
            Console.WriteLine($"Student Id | Name |  Class "); ;
            Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName} | {studentRecord.ClassName} "); ;
            Console.ReadKey();

        }
    }
}
