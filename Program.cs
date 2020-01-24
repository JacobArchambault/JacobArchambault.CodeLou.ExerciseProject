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
            string enterRecordMessage = "Press 1 to enter another student record. Press any other key to exit the program";
            List<Student> listOfStudents = new List<Student> { };

            do
            {
                listOfStudents.Add(GetStudentInfoFromUser());
                Console.WriteLine(enterRecordMessage);
                _ = int.TryParse(Console.ReadLine(), out keepGoing);
            } while (keepGoing == 1);

            listOfStudents.ForEach(s => PrintRecord(s));
        }
        static Student GetStudentInfoFromUser()
        {
            int studentId;
            DateTimeOffset lastCompletedOn;
            DateTimeOffset startDate;

            #region string resource table
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
        static void PrintMenu()
        {
            Console.WriteLine("Welcome to your student center. Please enter a number to select an option from the menu: ");
            Console.WriteLine("1. Enter a new student record.");
            Console.WriteLine("2. View a list of all students.");
            Console.WriteLine("3. Search for a student by name.");
            Console.WriteLine("To exit the program, press any other key.");
        }
    }
}