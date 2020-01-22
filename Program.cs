﻿using System;
using System.Collections.Generic;

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
            foreach (Student s in listOfStudents)
                PrintStudentRecord(s);
        }
        static Student GetStudentInfoFromUser()
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
            _ = DateTimeOffset.TryParse(Console.ReadLine(), out DateTimeOffset lastCompletedOn);

            Console.WriteLine("Enter Start Date in format MM/dd/YYYY");
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
        static void PrintStudentRecord(Student studentRecord)
        {
            Console.WriteLine($"Student Id \t| First Name \t| Last Name \t|  Class ");
            Console.WriteLine($"{studentRecord.StudentId} \t| {studentRecord.FirstName} \t| {studentRecord.LastName} \t| {studentRecord.ClassName} ");
        }
    }
}