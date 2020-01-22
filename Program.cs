using System;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    class Program
    {
        static void Main()
        {
            int keepGoing;
            do
            {
                Console.WriteLine("Enter Student Id");
                _ = int.TryParse(Console.ReadLine(), out int studentId);
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
                Console.WriteLine($"Student Id \t| Name \t|  Class ");
                Console.WriteLine($"{studentRecord.StudentId} \t| {studentRecord.FirstName} {studentRecord.LastName} \t| {studentRecord.ClassName} ");
                Console.WriteLine("Press 1 to enter another student record. Press any other key to exit the program");
                _ = int.TryParse(Console.ReadLine(), out keepGoing);
            } while (keepGoing == 1);

        }
    }
}
