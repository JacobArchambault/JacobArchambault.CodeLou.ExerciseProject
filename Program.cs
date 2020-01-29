using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using static JacobArchambault.CodeLou.ExerciseProject.Menu;
namespace JacobArchambault.CodeLou.ExerciseProject
{
    static class Program
    {

        static async Task Main()
        {
            string jsonFile = @"..\..\..\student.json";
            int response;
            List<Student> listOfStudents;

            using (FileStream fs = File.OpenRead(jsonFile))
            {

                listOfStudents = await JsonSerializer.DeserializeAsync<List<Student>>(fs);
            }

            Console.WriteLine("Welcome to your student center.");

            do
            {
                PrintMainMenu();
                _ = int.TryParse(Console.ReadLine(), out response);
                Choose(response, listOfStudents);
            } while (response == 1 || response == 2 || response == 3);

            using (FileStream fs = File.Create(jsonFile))
            {
                await JsonSerializer.SerializeAsync(fs, listOfStudents);
            }
        }
    }
}