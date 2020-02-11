using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    internal static class Menu
    {
        #region 1 Void methods
        internal static void PrintMainMenu()
        {

            Console.WriteLine("Please enter a number to select an option from the menu: ");
            Console.WriteLine("1. Enter a new student record.");
            Console.WriteLine("2. View a list of all students.");
            Console.WriteLine("3. Search for a student by name.");
            Console.WriteLine("Press any other key to exit the program.");
        }
        internal static void Choose(int option, List<Student> studentList)
        {
            List<Student> returnedList;
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
                    returnedList = Search(Console.ReadLine(), studentList);
                    if (returnedList.Any())
                    { 
                        returnedList.ForEach(s => PrintRecord(s)); 
                    }
                    else 
                    { 
                        Console.WriteLine("No students with that name were found."); 
                    }

                    break;
            }
        }
        #endregion
        #region 2 Return methods
        internal static Student GetStudentInfoFromUser()
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
        internal static void PrintRecord<T>(T record)
        {
            PropertyInfo[] objectInfo = record.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var propInfo in objectInfo)
                Console.WriteLine($"{propInfo.Name}: \t{propInfo.GetValue(record, null)}");

            Console.WriteLine();
        }
        internal static List<Student> Search(string userName, List<Student> listToSearchIn)
        {
            return (from l in listToSearchIn where l.Name.ToUpperInvariant().Contains(userName.ToUpperInvariant()) select l).ToList();
        }
        ///<summary>
        /// Prompts a user with a message and attempts to parse the user's response to a desired type.
        ///</summary>
        internal static bool GetStudentInput<T>(string userPrompt, out T output, TryParseHandler<T> handler)
        {
            Console.WriteLine(userPrompt);
            return handler(Console.ReadLine(), out output);
        }
        #endregion

        internal delegate bool TryParseHandler<T>(string value, out T result);

    }
}
