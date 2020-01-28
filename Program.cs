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
                Menu.PrintMainMenu();
                _ = int.TryParse(Console.ReadLine(), out int response);
                Menu.Choose(response, listOfStudents);
                Console.WriteLine("Press 1 to return to the main menu. Press any other key to exit the program");
                _ = int.TryParse(Console.ReadLine(), out keepGoing);
            } while (keepGoing == 1);
        }
    }
}