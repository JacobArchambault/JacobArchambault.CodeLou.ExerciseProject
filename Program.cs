using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    static class Program
    {

        static void Main()
        {
            List<Student> listOfStudents = new List<Student> { };
            int keepGoing;

            Console.WriteLine("Welcome to your student center.");

            do
            {
                PrintMainMenu();
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
                    Search(Console.ReadLine(), studentList).ForEach(s => PrintRecord(s));
                    break;
            }
            return studentList;
        }
        static Student GetStudentInfoFromUser()
        {
            string invalidDateMessage = "Invalid input format. Please enter the date in format MM/dd/YYYY";
            string completedPrompt = "When did you complete this class? Enter the date in format MM/dd/YYYY: ";
            string startDatePrompt = "Enter the date you wish to start on, in format MM/dd/YYYY: ";
            int studentId;
            DateTimeOffset lastCompletedOn;
            DateTimeOffset startDate;

            while (!(GetStudentInput("Enter your student ID number: ", out studentId, int.TryParse)))
            {
                Console.WriteLine("Invalid input. You must enter a non-zero whole number");
            }

            Console.WriteLine("Enter your first name: ");
            string studentFirstName = Console.ReadLine();

            Console.WriteLine("Enter your last name: ");
            string studentLastName = Console.ReadLine();

            Console.WriteLine("Enter the class you want to attend: ");
            string className = Console.ReadLine();

            Console.WriteLine("Enter the last class you completed: ");
            string lastClass = Console.ReadLine();

            while (!(GetStudentInput(completedPrompt, out lastCompletedOn, DateTimeOffset.TryParse)))
            {
                Console.WriteLine(invalidDateMessage);

            };

            while (!(GetStudentInput(startDatePrompt, out startDate, DateTimeOffset.TryParse)))
            {
                Console.WriteLine(invalidDateMessage);
            }

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
            return (from l in listToSearchIn where l.Name.ToUpperInvariant().Contains(userName.ToUpperInvariant()) select l).ToList();
        }
        delegate bool TryParseHandler<T>(string value, out T result);

        ///<summary>
        /// Prompts a user with a message and attempts to parse the user's response to a desired type.
        ///</summary>
        static bool GetStudentInput<T>(string userPrompt, out T output, TryParseHandler<T> handler)
        {
            Console.WriteLine(userPrompt);
            return handler(Console.ReadLine(), out output);
        }
    }
}