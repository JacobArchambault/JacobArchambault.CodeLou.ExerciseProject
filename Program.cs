using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    class Program
    {

        static void Main()
        {
            List<Student> listOfStudents = new List<Student> { };
            int keepGoing;

            Console.WriteLine("Welcome to your student center.");

            do
            {
                PrintMainMenu()
                _ = int.TryParse(Console.ReadLine(), out int response);
                Choose(response, listOfStudents);
                Console.WriteLine("Press 1 to return to the main menu. Press any other key to exit the program");
                _ = int.TryParse(Console.ReadLine(), out keepGoing);
            } while (keepGoing == 1);
        }
        static void PrintMainMenu()
        {

            Console.WriteLine("Please enter a number to select an option from the menu: ");
            Console.WriteLine("1. Enter a new student record.");
            Console.WriteLine("2. View a list of all students.");
            Console.WriteLine("3. Search for a student by name.");
            Console.WriteLine("Press any other key to exit the program.");
        }
        static List<Student> Choose(int option, List<Student> studentList)
        {
            switch (option)
            {
                case 1:
                    studentList.Add(GetStudentInfoFromUser());
                    break;
                case 2:
                    studentList.ForEach(s => PrintRecord(s));
                    break;
                case 3:
                    Console.WriteLine("Enter the name that you want to search for: ");
                    string userSearch = Console.ReadLine();
                    Search(userSearch, studentList).ForEach(s => PrintRecord(s));
                    break;
            }
            return studentList;
        }
        static Student GetStudentInfoFromUser()
        {
            int studentId;
            DateTimeOffset startDate;
            string invalidDateMessage = "Invalid input format. Please enter the date in format MM/dd/YYYY";

            do
            {
                Console.WriteLine("Enter Your student ID number: ");
                _ = int.TryParse(Console.ReadLine(), out studentId);
                if (studentId == 0)
                {
                    Console.WriteLine("Invalid input. You must enter a non-zero whole number");
                }
            } while (studentId == 0);

            Console.WriteLine("Enter your first name: ");
            string studentFirstName = Console.ReadLine();

            Console.WriteLine("Enter your last name: ");
            string studentLastName = Console.ReadLine();

            Console.WriteLine("Enter the class you want to attend: ");
            string className = Console.ReadLine();

            Console.WriteLine("Enter the last class you completed: ");
            string lastClass = Console.ReadLine();

            Console.WriteLine("When did you complete this class? Enter the date in format MM/dd/YYYY: ");
            _ = DateTimeOffset.TryParse(Console.ReadLine(), out DateTimeOffset lastCompletedOn);
            while (lastCompletedOn.Equals(DateTimeOffset.MinValue))
            {
                Console.WriteLine(invalidDateMessage);
                Console.WriteLine("When did you complete this class? Enter the date in format MM/dd/YYYY: ");
                _ = DateTimeOffset.TryParse(Console.ReadLine(), out lastCompletedOn);

            }; // Find a way to clean up the repetition in these lines and in those immediately following it.

            do
            {
                Console.WriteLine("Enter the date you wish to start on, in format MM/dd/YYYY: ");
                _ = DateTimeOffset.TryParse(Console.ReadLine(), out startDate);
                if (startDate.Equals(DateTimeOffset.MinValue))
                {
                    Console.WriteLine(invalidDateMessage);
                }
            } while (startDate.Equals(DateTimeOffset.MinValue));

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
        static List<Student> Search(string userName, List<Student> listToSearchIn) 
        {
            return (from l in listToSearchIn where l.Name.ToLowerInvariant().Contains(userName.ToLowerInvariant()) select l).ToList();
        }
    }
}