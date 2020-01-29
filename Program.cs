using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static JacobArchambault.CodeLou.ExerciseProject.Menu;
using static System.Console;
using static System.IO.File;
using static System.Text.Json.JsonSerializer;

namespace JacobArchambault.CodeLou.ExerciseProject
{
    static class Program
    {

        static async Task Main()
        {
            string jsonFile = @"..\..\..\student.json";
            int response;
            List<Student> listOfStudents;

            using (FileStream fs = OpenRead(jsonFile))
            {
                // TODO: Add checks to ensure json file exists and has the right format.
                listOfStudents = await DeserializeAsync<List<Student>>(fs); 
            }

            WriteLine("Welcome to your student center.");

            do
            {
                PrintMainMenu();
                _ = int.TryParse(ReadLine(), out response);
                Choose(response, listOfStudents);
            } while (response == 1 || response == 2 || response == 3);

            using (FileStream fs = Create(jsonFile))
            {
                // TODO: Add checks to ensure json file exists and has the right format.
                await SerializeAsync(fs, listOfStudents);
            }
        }
    }
}