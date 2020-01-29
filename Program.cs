using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static JacobArchambault.CodeLou.ExerciseProject.Menu;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    static class Program
    {

        static void Main()
        {
            string jsonFile = @"..\..\..\student.json";
            Console.WriteLine(File.Exists(jsonFile) ? "File exists." : "File does not exist.");
            List<Student> listOfStudents = new List<Student> { };
            int response;

            Console.WriteLine("Welcome to your student center.");

            do
            {
                PrintMainMenu();
                _ = int.TryParse(Console.ReadLine(), out response);
                Choose(response, listOfStudents);
            } while (response == 1 || response == 2 || response == 3);

            //_ = JsonSerializer.Serialize(listOfStudents);
        }
    }
}