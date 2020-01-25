using System;
using System.Collections.Generic;
using System.Reflection;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    class Program
    {

        static void Main()
        {

            int keepGoing;

            Console.WriteLine("Welcome to your student center.");

            do
            {
                PrintMainMenu();
                Console.WriteLine("Press 1 to return to the main menu. Press any other key to exit the program");
                _ = int.TryParse(Console.ReadLine(), out keepGoing);
            } while (keepGoing == 1);
        }
        static Student GetStudentInfoFromUser()
        {
            int studentId;
            DateTimeOffset lastCompletedOn;
            DateTimeOffset startDate;

            #region strings
            string enterId = "Enter Your student ID number: ";
            string invalidNumberMessage = "Invalid input. You must enter a non-zero whole number";
            string enterFirstName = "Enter your first name: ";
            string enterLastName = "Enter your last name: ";
            string enterClass = "Enter the class you want to attend: ";
            string enterLastClassCompleted = "Enter the last class you completed: ";
            string dateCompletedPrompt = "When did you complete this class? Enter the date in format MM/dd/YYYY: ";
            string invalidDateMessage = "Invalid input format. Please enter the date in format MM/dd/YYYY";
            string enterStartDate = "Enter the date you wish to start on, in format MM/dd/YYYY: ";
            #endregion
            do
            {
                Console.WriteLine(enterId);
                _ = int.TryParse(Console.ReadLine(), out studentId);
                if (studentId == 0)
                {
                    Console.WriteLine(invalidNumberMessage);
                }
            } while (studentId == 0);

            Console.WriteLine(enterFirstName);
            string studentFirstName = Console.ReadLine();

            Console.WriteLine(enterLastName);
            string studentLastName = Console.ReadLine();

            Console.WriteLine(enterClass);
            string className = Console.ReadLine();

            Console.WriteLine(enterLastClassCompleted);
            string lastClass = Console.ReadLine();
            do
            {
                Console.WriteLine(dateCompletedPrompt);
                _ = DateTimeOffset.TryParse(Console.ReadLine(), out lastCompletedOn);
                if (lastCompletedOn.Equals(DateTimeOffset.MinValue))
                {
                    Console.WriteLine(invalidDateMessage);
                }
            } while (lastCompletedOn.Equals(DateTimeOffset.MinValue));

            do
            {
                Console.WriteLine(enterStartDate);
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
        static void PrintMainMenu()
        {
            List<Student> listOfStudents = new List<Student> { };

            Console.WriteLine("Please enter a number to select an option from the menu: ");
            Console.WriteLine("1. Enter a new student record.");
            Console.WriteLine("2. View a list of all students.");
            Console.WriteLine("3. Search for a student by name.");
            Console.WriteLine("Press any other key to exit the program.");
            _ = int.TryParse(Console.ReadLine(), out int response);

            switch (response)
            {
                case 1:
                    listOfStudents.Add(GetStudentInfoFromUser());
                    break;
                case 2:
                    listOfStudents.ForEach(s => PrintRecord(s));
                    break;
                case 3:
                    Console.WriteLine("We're sorry, this option hasn't been completed yet.");
                    break;
                default: return;
            }
        }
        static List<Student> Search(string userName, List<Student> listToSearchIn) 
        {
            return listToSearchIn;
        }
    }
}