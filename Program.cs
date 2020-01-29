﻿using System;
using System.Collections.Generic;
using static JacobArchambault.CodeLou.ExerciseProject.Menu;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    static class Program
    {

        static void Main()
        {
            List<Student> listOfStudents = new List<Student> { };
            int response;

            Console.WriteLine("Welcome to your student center.");

            do
            {
                PrintMainMenu();
                _ = int.TryParse(Console.ReadLine(), out response);
                Choose(response, listOfStudents);
            } while (response == 1 || response == 2 || response == 3);
        }
    }
}