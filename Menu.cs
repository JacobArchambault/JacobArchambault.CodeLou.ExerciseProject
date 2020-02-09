using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Console;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    internal static class Menu
    {
        #region 1 Void methods
        internal static void PrintMainMenu()
        {

            WriteLine("Please enter a number to select an option from the menu: ");
            WriteLine("1. Enter a new student record.");
            WriteLine("2. View a list of all students.");
            WriteLine("3. Search for a student by name.");
            WriteLine("Press any other key to exit the program.");
        }
        internal static void Choose(int option, List<Student> studentList)
        {
            switch (option)
            {
                case 1:
                    studentList.Add(GetStudentInfoFromUser(studentList));
                    break;
                case 2:
                    studentList.ForEach(s => PrintRecord(s));
                    break;
                case 3:
                    WriteLine("Enter the name that you want to search for: ");
                    Search(ReadLine(), studentList).ForEach(s => PrintRecord(s));
                    break;
            }
        }
        /// <summary>
        /// Gets and prints data for all public properties of the object whose record is sought.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record"></param>
        internal static void PrintRecord<T>(T record)
        {
            PropertyInfo[] objectInfo = record.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propInfo in objectInfo)
                WriteLine($"{propInfo.Name}: \t{propInfo.GetValue(record, null)}");

            WriteLine();
        }

        #endregion

        #region 2 Return methods
        internal static Student GetStudentInfoFromUser(List<Student> listToCheckAgainst)
        {
            string invalidDateMessage = "Invalid input format. Please enter a valid date.";
            DateTimeOffset lastCompletedOn;
            DateTimeOffset startDate;
            Student student = new Student { };

            while (true)
            {
                if (!GetStudentInput("Enter a student ID number", out int studentId, int.TryParse))
                { 
                    WriteLine("Invalid input. You must enter a whole number"); 
                }
                else if ((Search(studentId, listToCheckAgainst)) != default)
                { 
                    WriteLine("A user with that ID number already exists"); 
                }
                else
                { 
                    student.StudentId = studentId; break; 
                }
            }

            WriteLine("Enter your first name: ");
            student.FirstName = ReadLine();

            WriteLine("Enter your last name: ");
            student.LastName = ReadLine();

            WriteLine("Enter the class you want to attend: ");
            student.ClassName = ReadLine();

            WriteLine("Enter the last class you completed: ");
            student.LastClassCompleted = ReadLine();

            while (!GetStudentInput("When did you complete this class? Enter the date in format MM/dd/YYYY: ", out lastCompletedOn, DateTimeOffset.TryParse))
                WriteLine(invalidDateMessage);

            student.LastClassCompletedOn = lastCompletedOn;
            
            while (true)
            {
                if (!GetStudentInput("Enter the date you wish to start on, in format MM/dd/YYYY: ", out startDate, DateTimeOffset.TryParse))
                {
                    WriteLine(invalidDateMessage);
                }
                else if (startDate < student.LastClassCompletedOn)
                {
                    WriteLine("The startDate for your new class must be later than your last class' completion date.");
                }
                else
                {
                    break;
                }
            }
            student.StartDate = startDate;

            return student;
        }

        /// <summary>
        /// Searches a list of students by name, and returns a list of those whose names include that string.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="listToSearchIn"></param>
        /// <returns></returns>
        internal static List<Student> Search(string userName, List<Student> listToSearchIn) => 
            (from l in listToSearchIn where l.Name.ToUpperInvariant().Contains(userName.ToUpperInvariant()) select l).ToList();

        /// <summary>
        /// Searches for a student by studentId and returns the first exact match.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="listToSearchIn"></param>
        /// <returns></returns>
        internal static Student Search(int studentId, List<Student> listToSearchIn) =>
            (from l in listToSearchIn where l.StudentId.Equals(studentId) select l).FirstOrDefault();

        /// <summary>
        /// Prompts a user with a message and parses the user's response to a desired type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userPrompt"></param>
        /// <param name="output"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        internal static bool GetStudentInput<T>(string userPrompt, out T output, TryParseHandler<T> handler)
        {
            WriteLine(userPrompt);
            return handler(ReadLine(), out output);
        }
        internal delegate bool TryParseHandler<T>(string value, out T result);
        #endregion
    }
}