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
        internal static void PrintRecord<T>(T record)
        {
            PropertyInfo[] objectInfo = record.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var propInfo in objectInfo)
                WriteLine($"{propInfo.Name}: \t{propInfo.GetValue(record, null)}");

            WriteLine();
        }

        #endregion

        #region 2 Return methods
        internal static Student GetStudentInfoFromUser(List<Student> listToCheckAgainst)
        {
            string invalidDateMessage = "Invalid input format. Please enter the date in format MM/dd/YYYY";
            DateTimeOffset lastCompletedOn;
            DateTimeOffset startDate;
            Student student = new Student { };

            while (true)
            {
                if (!GetStudentInput("Enter a student ID number", out int studentId, int.TryParse))
                { 
                    WriteLine("Invalid input. You must enter a non-zero whole number"); 
                }
                else if (Search(studentId, listToCheckAgainst).Any())
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

            while (!(GetStudentInput("When did you complete this class? Enter the date in format MM/dd/YYYY: ", out lastCompletedOn, DateTimeOffset.TryParse)))
                WriteLine(invalidDateMessage);

            student.LastClassCompletedOn = lastCompletedOn;
            
            while (true)
            {
                if (!(GetStudentInput("Enter the date you wish to start on, in format MM/dd/YYYY: ", out startDate, DateTimeOffset.TryParse)))
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
        internal static List<Student> Search(string userName, List<Student> listToSearchIn)
        {
            return (from l in listToSearchIn where l.Name.ToUpperInvariant().Contains(userName.ToUpperInvariant()) select l).ToList();
        }
        internal static List<Student> Search(int studentId, List<Student> listToSearchIn)
        {
            return (from l in listToSearchIn where l.StudentId.Equals(studentId) select l).ToList();
        }

        ///<summary>
        /// Prompts a user with a message and parses the user's response to a desired type.
        ///</summary>
        internal static bool GetStudentInput<T>(string userPrompt, out T output, TryParseHandler<T> handler)
        {
            WriteLine(userPrompt);
            return handler(ReadLine(), out output);
        }
        internal delegate bool TryParseHandler<T>(string value, out T result);
        #endregion
    }
}